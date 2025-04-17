using System.Collections.ObjectModel;
using System.Text;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.Framework.Core.Mail;
using Microsoft.AspNetCore.WebUtilities;
using NexKoala.Framework.Core.Identity.Users.Features.ResetPassword;
using NexKoala.Framework.Core.Identity.Users.Features.ForgotPassword;
using NexKoala.Framework.Core.Identity.Users.Features.ChangePassword;

namespace NexKoala.Framework.Infrastructure.Identity.Users.Services;
internal sealed partial class UserService
{
    public async Task ForgotPasswordAsync(ForgotPasswordCommand request, string origin, CancellationToken cancellationToken)
    {
        EnsureValidTenant();

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException("user not found");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new InvalidOperationException("user email cannot be null or empty");
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        if (!origin.EndsWith("/"))
        {
            origin += "/";
        }

        var resetPasswordUrl = $"{origin}user/reset-password?token={token}&email={request.Email}";

        var emailBody = $@"
                <html>
                    <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; color: #333; padding: 20px;'>
                        <div style='max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 8px;'>
                            <h2 style='text-align: center;'>Password Reset Request</h2>
                            <p>Hi {user.FirstName},</p>
                            <p>We received a request to reset your password. You can reset your password by clicking the link below:</p>
                            <p style='text-align: center;'>
                                <a href='{resetPasswordUrl}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>
                                    Reset Password
                                </a>
                            </p>
                            <p>If you did not request this, please ignore this email. Your password will remain unchanged.</p>
                            <p>If you have any questions, feel free to reach out to our support team at 
                                <a href='mailto:contactus@nexkoala.com.my'>contactus@nexkoala.com.my</a>.</p>
                            <p>Best regards,<br/>The Nex Koala e-Invoice Team</p>
                        </div>
                    </body>
                </html>";

        var mailRequest = new MailRequest(
            new Collection<string> { user.Email },
            "Reset Password",
            emailBody);

        jobService.Enqueue(() => mailService.SendAsync(mailRequest, CancellationToken.None));
    }

    public async Task ResetPasswordAsync(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        EnsureValidTenant();

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException("user not found");
        }

        request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
        var result = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new GenericException("error resetting password", errors);
        }
    }

    public async Task ChangePasswordAsync(ChangePasswordCommand request, string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException("user not found");

        var result = await userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new GenericException("failed to change password", errors);
        }
    }
}
