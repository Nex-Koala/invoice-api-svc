<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes" />

	<xsl:template match="/">
		<html>
			<head>
				<style>
					body {
						font-family: system-ui, -apple-system, BlinkMacSystemFont,
							"Segoe UI", Roboto, Oxygen, Ubuntu, Cantarell, "Open Sans",
							"Helvetica Neue", sans-serif, sans-serif;
						margin: 0 auto;
						width: 8.5in;
						background: white;
						padding: 10px 20px;
						box-sizing: border-box;
						font-size: 12px;
					}
					main {
						display: block;
						justify-content: center;
					}
					h1,
					h2 {
						margin: 0;
					}
					p {
						margin: 0;
					}
					.header {
						text-align: center;
						margin-bottom: 1rem;
					}
					.supplier-invoice-container {
						display: table;
						width: 100%;
					}
					.supplier-info,
					.invoice-info {
						display: table-cell;
						vertical-align: bottom;
					}
					.supplier-info {
						text-align: left;
						width: 50%;
					}
					.invoice-info {
						text-align: right;
						width: 50%;
					}
					.separator {
						border-color: rgb(211, 211, 211);
						margin: 10px 0;
					}
					.supplier-buyer-info,
					.footer {
						margin-top: 20px;
						font-size: 14px;
					}
					.supplier-buyer-info p,
					.footer p {
						margin: 5px 0;
					}
					.table-container {
						margin-top: 20px;
					}
					table {
						width: 100%;
						border-collapse: collapse;
					}
					table,
					th,
					td {
						border: 1px solid rgb(211, 211, 211);
						font-size: 12px;
					}
					th {
						padding: 8px;
						text-align: center;
						color: white;
						background-color: black;
						font-weight: 600;
					}
					td {
						padding: 3px 5px;
					}
					.summaryRowTitle {
						text-align: left;
						font-weight: 700;
					}
					.footer {
						margin-top: 30px;
						display: table;
						width: 100%;
						font-size: 12px;
					}
					.footer div {
						display: table-cell;
						width: 50%;
					}
					.footer .qr-code {
						text-align: right;
					}
					.qr-img {
						width: 100px;
						height: 100px;
						max-width: 100%;
					}
				</style>
			</head>
			<body>
				<main>
					<div class="header">
						<b><xsl:value-of select="invoice/supplierName" /></b>
						<p><xsl:value-of select="invoice/supplierAddress" /></p>
						<p><xsl:value-of select="invoice/supplierContact" /></p>
						<a href="mailto:{invoice/supplierEmail}"
							><xsl:value-of select="invoice/supplierEmail"
						/></a>
					</div>
					<div class="supplier-invoice-container">
						<div class="supplier-info">
							<p>Supplier TIN: 
								<xsl:choose>
									<xsl:when test="string(invoice/supplierTIN) != ''">
										<xsl:value-of select="invoice/supplierTIN" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Supplier Registration Number:
								<xsl:choose>
									<xsl:when test="string(invoice/supplierRegNo) != ''">
										<xsl:value-of select="invoice/supplierRegNo" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Supplier SST ID: 
								<xsl:choose>
									<xsl:when test="string(invoice/supplierSST) != ''">
										<xsl:value-of select="invoice/supplierSST" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Supplier MSIC code:
								<xsl:choose>
									<xsl:when test="string(invoice/supplierMSIC) != ''">
										<xsl:value-of select="invoice/supplierMSIC" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Supplier business activity description:
								<xsl:choose>
									<xsl:when test="string(invoice/businessDescription) != ''">
										<xsl:value-of select="invoice/businessDescription" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
						</div>

						<div class="invoice-info">
							<p><b>E-INVOICE</b></p>
							<p>e-Invoice Type: 
								<xsl:choose>
									<xsl:when test="string(invoice/eInvoiceType) != ''">
										<xsl:value-of select="invoice/eInvoiceType" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>e-Invoice version:
								<xsl:choose>
									<xsl:when test="string(invoice/eInvoiceVersion) != ''">
										<xsl:value-of select="invoice/eInvoiceVersion" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>e-Invoice code: 
								<xsl:choose>
									<xsl:when test="string(invoice/eInvoiceCode) != ''">
										<xsl:value-of select="invoice/eInvoiceCode" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Unique Identifier No: 
								<xsl:choose>
									<xsl:when test="string(invoice/uuid) != ''">
										<xsl:value-of select="invoice/uuid" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Original Invoice Ref. No.: 
								<xsl:choose>
									<xsl:when test="string(invoice/originalInvoiceRefNo) != ''">
										<xsl:value-of select="invoice/originalInvoiceRefNo" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Invoice Date and Time: 
								<xsl:choose>
									<xsl:when test="string(invoice/invoiceDateTime) != ''">
										<xsl:value-of select="invoice/invoiceDateTime" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
						</div>
					</div>

					<hr class="separator" />

					<div class="buyer-info">
						<p>Buyer TIN: 
							<xsl:choose>
								<xsl:when test="string(invoice/buyerTIN) != ''">
									<xsl:value-of select="invoice/buyerTIN" />
								</xsl:when>
								<xsl:otherwise>-</xsl:otherwise>
							</xsl:choose>
						</p>
						<p>Buyer Name: 
							<xsl:choose>
								<xsl:when test="string(invoice/buyerName) != ''">
									<xsl:value-of select="invoice/buyerName" />
								</xsl:when>
								<xsl:otherwise>-</xsl:otherwise>
							</xsl:choose>
						</p>
						<p>Buyer Identification Number:
							<xsl:choose>
								<xsl:when test="string(invoice/buyerRegNo) != ''">
									<xsl:value-of select="invoice/buyerRegNo" />
								</xsl:when>
								<xsl:otherwise>-</xsl:otherwise>
							</xsl:choose>
						</p>
						<p>Buyer Address: 
							<xsl:choose>
								<xsl:when test="string(invoice/buyerAddress) != ''">
									<xsl:value-of select="invoice/buyerAddress" />
								</xsl:when>
								<xsl:otherwise>-</xsl:otherwise>
							</xsl:choose>
						</p>
						<p>Buyer Contact Number (Mobile):
							<xsl:choose>
								<xsl:when test="string(invoice/buyerContactNumber) != ''">
									<xsl:value-of select="invoice/buyerContactNumber" />
								</xsl:when>
								<xsl:otherwise>-</xsl:otherwise>
							</xsl:choose>
						</p>
						<p>Buyer Email: 
							<xsl:choose>
								<xsl:when test="string(invoice/buyerEmail) != ''">
									<xsl:value-of select="invoice/buyerEmail" />
								</xsl:when>
								<xsl:otherwise>-</xsl:otherwise>
							</xsl:choose>
						</p>
					</div>


					<div class="table-container">
						<table>
							<thead>
								<tr>
									<th>Classification</th>
									<th>Description</th>
									<th>Quantity</th>
									<th>Unit Price</th>
									<th>Amount</th>
									<th>Disc</th>
									<th>Tax Rate</th>
									<th>Tax Amount</th>
									<th>Total Price / Service Price (incl. tax)</th>
								</tr>
							</thead>
							<tbody>
								<xsl:for-each select="invoice/items/item">
									<tr>
										<td style="text-align: center">
											<xsl:choose>
												<xsl:when test="string(classificationCode) != ''">
													<xsl:value-of select="classificationCode" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: left">
											<xsl:choose>
												<xsl:when test="string(description) != ''">
													<xsl:value-of select="description" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: center">
											<xsl:choose>
												<xsl:when test="string(quantity) != ''">
													<xsl:value-of select="quantity" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: right">
											<xsl:choose>
												<xsl:when test="string(unitPrice) != ''">
													<xsl:value-of select="currencyCode" />
													<xsl:value-of select="unitPrice" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: right">
											<xsl:choose>
												<xsl:when test="string(amount) != ''">
													<xsl:value-of select="currencyCode" />
													<xsl:value-of select="amount" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: center">
											<xsl:choose>
												<xsl:when test="string(discount) != ''">
													<xsl:value-of select="currencyCode" />
													<xsl:value-of select="discount" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: center">
											<xsl:choose>
												<xsl:when test="string(taxRate) != ''">
													<xsl:value-of select="currencyCode" />
													<xsl:value-of select="taxRate" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: center">
											<xsl:choose>
												<xsl:when test="string(taxAmount) != ''">
													<xsl:value-of select="currencyCode" />
													<xsl:value-of select="taxAmount" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="text-align: right">
											<xsl:choose>
												<xsl:when test="string(totalPrice) != ''">
													<xsl:value-of select="currencyCode" />
													<xsl:value-of select="totalPrice" />
												</xsl:when>
												<xsl:otherwise>-</xsl:otherwise>
											</xsl:choose>
										</td>
									</tr>
								</xsl:for-each>
								<tr>
									<td colspan="2" style="border: none"></td>
									<td colspan="6" class="summaryRowTitle">Subtotal</td>
									<td style="text-align: right">
										<xsl:choose>
											<xsl:when test="string(invoice/subtotal) != ''">
												<xsl:value-of select="invoice/currencyCode" />
												<xsl:value-of select="invoice/subtotal" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
								</tr>
								<tr>
									<td colspan="2" style="border: none"></td>
									<td colspan="6" class="summaryRowTitle">Total excluding tax</td>
									<td style="text-align: right">
										<xsl:choose>
											<xsl:when test="string(invoice/totalExcludingTax) != ''">
												<xsl:value-of select="invoice/currencyCode" />
												<xsl:value-of select="invoice/totalExcludingTax" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
								</tr>
								<tr>
									<td colspan="2" style="border: none"></td>
									<td colspan="6" class="summaryRowTitle">Tax amount</td>
									<td style="text-align: right">
										<xsl:choose>
											<xsl:when test="string(invoice/taxAmount) != ''">
												<xsl:value-of select="invoice/currencyCode" />
												<xsl:value-of select="invoice/taxAmount" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
								</tr>
								<tr>
									<td colspan="2" style="border: none"></td>
									<td colspan="6" class="summaryRowTitle">Total including tax</td>
									<td style="text-align: right">
										<xsl:choose>
											<xsl:when test="string(invoice/totalIncludingTax) != ''">
												<xsl:value-of select="invoice/currencyCode" />
												<xsl:value-of select="invoice/totalIncludingTax" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
								</tr>
								<tr>
									<td colspan="2" style="border: none"></td>
									<td colspan="6" class="summaryRowTitle">Total payable amount</td>
									<td style="text-align: right">
										<xsl:choose>
											<xsl:when test="string(invoice/totalPayableAmount) != ''">
												<xsl:value-of select="invoice/currencyCode" />
												<xsl:value-of select="invoice/totalPayableAmount" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
								</tr>
							</tbody>
						</table>
					</div>

					<div class="footer">
						<div>
							<p>
								Digital Signature:<br /><xsl:value-of
									select="invoice/digitalSignature"
								/>
							</p>
							<p>
								Date and Time of Validation:
								<xsl:value-of select="invoice/dateTimeValidated" />
							</p>
							<p>This document is a visual presentation of the e-invoice</p>
						</div>
						<div class="qr-code">
							<xsl:if test="invoice/QRCode != ''">
                            <img class="qr-img">
                                <xsl:attribute name="src">
                                    <xsl:value-of select="invoice/QRCode" />
                                </xsl:attribute>
                                <xsl:attribute name="alt">Invoice QR Code</xsl:attribute>
                            </img>
                        </xsl:if>
						</div>
					</div>
				</main>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
