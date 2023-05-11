# Web api for receiving and replying to twilio sms messages

## SmsController.cs
Http post endpoint that handles incoming messages from twilio.  Replies to twilio and logs the messages to the database.

## SmsCodeRequestsController.cs
Http get endpoint for a list of the most recent sms messages saved in the database.  Used for the UI.

## SmsCodeListResponseDto.cs and SmsCodeDto.cs
Data transfer objects for the response data from the UI endpoint SmsCodeRequestsController.cs.

