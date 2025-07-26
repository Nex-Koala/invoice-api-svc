<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes" />

	<xsl:template match="/">
		<html>

		<head>
			<style>
				body {
					font-family: Roboto, system-ui, -apple-system, BlinkMacSystemFont,
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
                    margin: 3px 0;
                }

				a {
					text-decoration: none;
					color: black;
				}

				.header {
					text-align: center;
					margin-bottom: 1rem;
				}

				.separator {
					border: 1px solid rgb(211, 211, 211);
					margin: 15px 0;
				}

				.qr-img {
					width: 100px;
					height: 100px;
					max-width: 100%;
				}

				.row {
					display: table;
					width: 100%;
				}

				.col {
					display: table-cell;
					vertical-align: top;
				}

				.text-right {
					text-align: right;
				}

				.col-title {
					background-color: rgb(231, 234, 236);
					padding: 5px 0;
					font-weight: 800;
				}

				.inner-col {
					margin-right: 10px;
				}

				.col:last-child .inner-col {
					margin-right: 0;
				}

				.font-bold {
					font-weight: 700;
				}

				.table-container {
					margin: 20px 0 10px;
				}

				table {
					border-collapse: collapse;
				}

				.table-container table td,
				.table-container table th,
				.updown-border {
					border-top: 1px solid rgb(187, 192, 196);
					border-bottom: 1px solid rgb(187, 192, 196);
				}

				.table-container table tr th {
					font-size: 14px;
					background-color: rgb(231, 234, 236);
					padding: 5px 8px;
					text-align: left;
				}

				.table-container table tr td {
					text-align: left;
					padding: 5px 8px;
				}

				.table-total-title {
					font-weight: 700;
					font-size: 14px;
				}

				.footer p {
					margin: 10px 0;
				}
			</style>
		</head>

		<body>
			<main>
				<div class="header">
					<h1>INVOICE</h1>
				</div>

				<div class="row">
					<div class="col" style="width: 50%;">
						<b><xsl:value-of select="invoice/supplierName" /></b>
						<p><xsl:value-of select="invoice/supplierAddress" /></p>
						<p><xsl:value-of select="invoice/supplierContact" /></p>
						<a href="mailto:{invoice/supplierEmail}">
							<xsl:value-of select="invoice/supplierEmail"/>
						</a>
					</div>
					<div class="col text-right">
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

				<hr class="separator" />

				<div class="row">
					<div class="col">
						<div class="inner-col">
							<p class="col-title">Supplier</p>
							<p>TIN:
								<xsl:choose>
									<xsl:when test="string(invoice/supplierTIN) != ''">
										<xsl:value-of select="invoice/supplierTIN" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Registration Number:
								<xsl:choose>
									<xsl:when test="string(invoice/supplierRegNo) != ''">
										<xsl:value-of select="invoice/supplierRegNo" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>SST ID:
								<xsl:choose>
									<xsl:when test="string(invoice/supplierSST) != ''">
										<xsl:value-of select="invoice/supplierSST" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>MSIC code:
								<xsl:choose>
									<xsl:when test="string(invoice/supplierMSIC) != ''">
										<xsl:value-of select="invoice/supplierMSIC" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Business Activity Description:
								<xsl:choose>
									<xsl:when test="string(invoice/businessDescription) != ''">
										<xsl:value-of select="invoice/businessDescription" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
						</div>
					</div>
					<div class="col">
						<div class="inner-col">
							<p class="col-title">Buyer</p>
							<p>TIN:
								<xsl:choose>
									<xsl:when test="string(invoice/buyerTIN) != ''">
										<xsl:value-of select="invoice/buyerTIN" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Name:
								<xsl:choose>
									<xsl:when test="string(invoice/buyerName) != ''">
										<xsl:value-of select="invoice/buyerName" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Identification Number:
								<xsl:choose>
									<xsl:when test="string(invoice/buyerRegNo) != ''">
										<xsl:value-of select="invoice/buyerRegNo" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Address:
								<xsl:choose>
									<xsl:when test="string(invoice/buyerAddress) != ''">
										<xsl:value-of select="invoice/buyerAddress" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Email:
								<xsl:choose>
									<xsl:when test="string(invoice/buyerEmail) != ''">
										<xsl:value-of select="invoice/buyerEmail" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
							<p>Contact Number:
								<xsl:choose>
									<xsl:when test="string(invoice/buyerContactNumber) != ''">
										<xsl:value-of select="invoice/buyerContactNumber" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</p>
						</div>
					</div>
					<div class="col text-right">
						<p>e-Invoice Type:
							<span class="font-bold">
								<xsl:choose>
									<xsl:when test="string(invoice/eInvoiceType) != ''">
										<xsl:value-of select="invoice/eInvoiceType" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</span>
						</p>
						<p>e-Invoice version:
							<span class="font-bold">
								<xsl:choose>
									<xsl:when test="string(invoice/eInvoiceVersion) != ''">
										<xsl:value-of select="invoice/eInvoiceVersion" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</span>
						</p>
						<p>e-Invoice code:
							<span class="font-bold">
								<xsl:choose>
									<xsl:when test="string(invoice/eInvoiceCode) != ''">
										<xsl:value-of select="invoice/eInvoiceCode" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</span>
						</p>
						<p>Unique Identifier No:
							<span class="font-bold">
								<xsl:choose>
									<xsl:when test="string(invoice/uuid) != ''">
										<xsl:value-of select="invoice/uuid" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</span>
						</p>
						<p>Original Invoice Ref. No.:
							<span class="font-bold">
								<xsl:choose>
									<xsl:when test="string(invoice/originalInvoiceRefNo) != ''">
										<xsl:value-of select="invoice/originalInvoiceRefNo" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</span>
						</p>
						<p>Invoice Date and Time:
							<span class="font-bold">
								<xsl:choose>
									<xsl:when test="string(invoice/invoiceDateTime) != ''">
										<xsl:value-of select="invoice/invoiceDateTime" />
									</xsl:when>
									<xsl:otherwise>-</xsl:otherwise>
								</xsl:choose>
							</span>
						</p>
					</div>
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
									<td>
										<xsl:choose>
											<xsl:when test="string(classificationCode) != ''">
												<xsl:value-of select="classificationCode" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="string(description) != ''">
												<xsl:value-of select="description" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="string(quantity) != ''">
												<xsl:value-of select="quantity" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="string(unitPrice) != ''">
												<xsl:value-of select="currencyCode" />
												<xsl:value-of select="unitPrice" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="string(amount) != ''">
												<xsl:value-of select="currencyCode" />
												<xsl:value-of select="amount" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="string(discount) != ''">
												<xsl:value-of select="currencyCode" />
												<xsl:value-of select="discount" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="string(taxRate) != ''">
												<xsl:value-of select="currencyCode" />
												<xsl:value-of select="taxRate" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="string(taxAmount) != ''">
												<xsl:value-of select="currencyCode" />
												<xsl:value-of select="taxAmount" />
											</xsl:when>
											<xsl:otherwise>-</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
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
								<td colspan="5"></td>
								<td colspan="3" class="table-total-title">Subtotal</td>
								<td>
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
								<td colspan="5"></td>
								<td colspan="3" class="table-total-title">Total excluding tax</td>
								<td>
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
								<td colspan="5"></td>
								<td colspan="3" class="table-total-title">Tax amount</td>
								<td>
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
								<td colspan="5"></td>
								<td colspan="3" class="table-total-title">Total including tax</td>
								<td>
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
								<td colspan="5"></td>
								<td colspan="3" class="table-total-title">Total payable amount</td>
								<td>
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
					<!--<p>
						Digital Signature:<br /><xsl:value-of select="invoice/digitalSignature" />
					</p>-->
					<p>
						Date and Time of Validation:
						<xsl:value-of select="invoice/dateTimeValidated" />
					</p>
					<p>This document is a visual presentation of the e-invoice</p>
				</div>
			</main>
		</body>

		</html>
	</xsl:template>
</xsl:stylesheet>