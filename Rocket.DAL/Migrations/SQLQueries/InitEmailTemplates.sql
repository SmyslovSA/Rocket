﻿INSERT INTO [dbo].[EmailTemplates]	([Title], [Body])
     VALUES
           (N'Premium',
           N'@model Rocket.BL.Common.Models.Notification.BillingMessage

		<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<html xmlns="http://www.w3.org/1999/xhtml" xmlns="http://www.w3.org/1999/xhtml" style="font-family: ''Helvetica Neue'', Helvetica, Arial, sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;">
		<head>
		<meta name="viewport" content="width=device-width" />
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<title>Billing e.g. invoices and receipts</title>

		<style>img {
		max-width: 100%;
		}
		body {
		-webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em;
		}
		body {
		background-color: #f6f6f6;
		}
		@@media only screen and (max-width: 640px) {
			body {
			padding: 0 !important;
			}
			h1 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h2 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h3 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h4 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h1 {
			font-size: 22px !important;
			}
			h2 {
			font-size: 18px !important;
			}
			h3 {
			font-size: 16px !important;
			}
			.container {
			padding: 0 !important; width: 100% !important;
			}
			.content {
			padding: 0 !important;
			}
			.content-wrap {
			padding: 10px !important;
			}
			.invoice {
			width: 100% !important;
			}
		}
		</style></head>

		<body itemscope="" itemtype="http://schema.org/EmailMessage" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em; margin: 0;" bgcolor="#f6f6f6">

		<table style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; margin: 0;" bgcolor="#f6f6f6"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" valign="top"></td>
				<td width="600" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;" valign="top">
					<div style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;">
						<table width="100%" cellpadding="0" cellspacing="0" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0; border: 1px solid #e9e9e9;" bgcolor="#fff"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 20px;" align="center" valign="top">
									<table width="100%" cellpadding="0" cellspacing="0" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" valign="top">
												<h1 style="font-family: ''Helvetica Neue'',Helvetica,Arial,''Lucida Grande'',sans-serif; box-sizing: border-box; font-size: 32px; color: #000; line-height: 1.2em; font-weight: 500; margin: 40px 0 0;" align="center">Оплата в размере @Model.Sum@Model.Currency успешно проведена</h1>
											</td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" valign="top">
												<h2 style="font-family: ''Helvetica Neue'',Helvetica,Arial,''Lucida Grande'',sans-serif; box-sizing: border-box; font-size: 24px; color: #000; line-height: 1.2em; font-weight: 400; margin: 40px 0 0;" align="center">Спасибо, что пользуетесь сервисом ROCKET</h2>
											</td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" align="center" valign="top">
												<table style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; text-align: left; width: 80%; margin: 40px auto;"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 5px 0;" valign="top">@Model.Receiver.Name<br style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" /><br style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" />@DateTime.Now</td>
													</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 5px 0;" valign="top">
															<table cellpadding="0" cellspacing="0" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; margin: 0;"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" valign="top">Оплата премиум аккаунта</td>
																	<td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" align="right" valign="top">@Model.Sum @Model.Currency</td>
																</tr></table></td>
													</tr></table></td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" align="center" valign="top">
												<a href="http://www.mailgun.com" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #348eda; text-decoration: underline; margin: 0;">Перейти на главную страницу ROCKET</a>
											</td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" align="center" valign="top">
							ул. Скрыганова, 14, 5-й этаж, г. Минск, Республика Беларусь                 
											</td>
										</tr></table></td>
							</tr></table><div style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;">					
				</td>
				<td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" valign="top"></td>
			</tr></table></body>
		</html>
			'),
			(N'UserDonate',
			N'@model BillingMessage

		<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<html xmlns="http://www.w3.org/1999/xhtml" xmlns="http://www.w3.org/1999/xhtml" style="font-family: ''Helvetica Neue'', Helvetica, Arial, sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;">
		<head>
		<meta name="viewport" content="width=device-width" />
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<title>Billing e.g. invoices and receipts</title>

		<style>img {
		max-width: 100%;
		}
		body {
		-webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em;
		}
		body {
		background-color: #f6f6f6;
		}
		@media only screen and (max-width: 640px) {
			body {
			padding: 0 !important;
			}
			h1 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h2 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h3 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h4 {
			font-weight: 800 !important; margin: 20px 0 5px !important;
			}
			h1 {
			font-size: 22px !important;
			}
			h2 {
			font-size: 18px !important;
			}
			h3 {
			font-size: 16px !important;
			}
			.container {
			padding: 0 !important; width: 100% !important;
			}
			.content {
			padding: 0 !important;
			}
			.content-wrap {
			padding: 10px !important;
			}
			.invoice {
			width: 100% !important;
			}
		}
		</style></head>

		<body itemscope="" itemtype="http://schema.org/EmailMessage" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em; margin: 0;" bgcolor="#f6f6f6">

		<table style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; margin: 0;" bgcolor="#f6f6f6"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" valign="top"></td>
				<td width="600" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;" valign="top">
					<div style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;">
						<table width="100%" cellpadding="0" cellspacing="0" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0; border: 1px solid #e9e9e9;" bgcolor="#fff"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 20px;" align="center" valign="top">
									<table width="100%" cellpadding="0" cellspacing="0" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" valign="top">
												<h1 style="font-family: ''Helvetica Neue'',Helvetica,Arial,''Lucida Grande'',sans-serif; box-sizing: border-box; font-size: 32px; color: #000; line-height: 1.2em; font-weight: 500; margin: 40px 0 0;" align="center">Оплата в размере @Model.Sum@Model.Currency успешно проведена</h1>
											</td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" valign="top">
												<h2 style="font-family: ''Helvetica Neue'',Helvetica,Arial,''Lucida Grande'',sans-serif; box-sizing: border-box; font-size: 24px; color: #000; line-height: 1.2em; font-weight: 400; margin: 40px 0 0;" align="center">Спасибо, что поддержали сервис ROCKET</h2>
											</td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" align="center" valign="top">
												<table style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; text-align: left; width: 80%; margin: 40px auto;"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 5px 0;" valign="top">@Model.Receiver.Name<br style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" /><br style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" />@DateTime.Now</td>
													</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 5px 0;" valign="top">
															<table cellpadding="0" cellspacing="0" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; margin: 0;"><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" valign="top">Донат</td>
																	<td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" align="right" valign="top">@Model.Sum @Model.Currency</td>
																</tr></table></td>
													</tr></table></td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" align="center" valign="top">
												<a href="http://www.mailgun.com" style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #348eda; text-decoration: underline; margin: 0;">Перейти на главную страницу ROCKET</a>
											</td>
										</tr><tr style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"><td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0; padding: 0 0 20px;" align="center" valign="top">
							ул. Скрыганова, 14, 5-й этаж, г. Минск, Республика Беларусь                 
											</td>
										</tr></table></td>
							</tr></table><div style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;">					
				</td>
				<td style="font-family: ''Helvetica Neue'',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" valign="top"></td>
			</tr></table></body>
		</html>
		');