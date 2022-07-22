
using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System.Collections;
using static Google.Apis.Sheets.v4.SpreadsheetsResource;

namespace SheetsQuickstart
{
    public class GoogleSheets
    {
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        static SheetsService service;


        private SpreadsheetsResource spreadsheet;
        //Connect with GSheet api.
        public GoogleSheets()
        {
            GoogleCredential credential;            
            using (var stream = new FileStream("GoogleCreds.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
            
            spreadsheet = new SpreadsheetsResource(service);
        }

        public string Create(string sheetName)
        {
            Spreadsheet test = new Spreadsheet();
            test.Properties = new SpreadsheetProperties();
            test.Properties.Title = sheetName;
            SpreadsheetsResource.CreateRequest request = spreadsheet.Create(test);

            Spreadsheet response = request.Execute();
            return JsonConvert.SerializeObject(response);
        }

        public string Append(string spreadsheetId, string worksheetName, IList<IList<object>> sheetValues)
        {

            // The A1 notation of a range to search for a logical table of data.
            // Values will be appended after the last row of the table.
            string range = worksheetName+ "!A1";  // TODO: Update placeholder value.

            // How the input data should be interpreted.
            SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum valueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;  // TODO: Update placeholder value.

            // How the input data should be inserted.
            SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum insertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;  // TODO: Update placeholder value.

            // TODO: Assign values to desired properties of `requestBody`:
            ValueRange requestBody = new ValueRange();

            //
            requestBody.Values = sheetValues;
            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(requestBody, spreadsheetId, range);
            request.ValueInputOption = valueInputOption;
            request.InsertDataOption = insertDataOption;

            // To execute asynchronously in an async method, replace `request.Execute()` as shown:
            AppendValuesResponse response = request.Execute();
            // Data.AppendValuesResponse response = await request.ExecuteAsync();

            // TODO: Change code below to process the `response` object:
            return JsonConvert.SerializeObject(response);
        }



        //Need to be able to create, update, and delete to be able to query. Create a new spreadsheet, update first cell with the query, read whole sheet, then delete sheet.



        public string Query(string spreadsheetId, string worksheetName)
        {
            //string range = $"{worksheetName}!B7:F35";
            //SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum valueRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum)0;  // TODO: Update placeholder value.
            // How dates, times, and durations should be represented in the output.
            // This is ignored if value_render_option is
            // FORMATTED_VALUE.
            // The default dateTime render option is [DateTimeRenderOption.SERIAL_NUMBER].
            //SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum dateTimeRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum)0;  // TODO: Update placeholder value.

 
            List<Request> requests = new List<Request>();
            BatchUpdateSpreadsheetRequest requestBody = new BatchUpdateSpreadsheetRequest()
            {
                IncludeSpreadsheetInResponse = true,
            };
            requestBody.Requests = requests;

            SpreadsheetsResource.BatchUpdateRequest request = service.Spreadsheets.BatchUpdate(requestBody,spreadsheetId);

            Request find = new Request()
            {
                FindReplace = new FindReplaceRequest()
                {
                    Find = "This One",
                    Replacement = "FOUND",
                    SheetId = 1736661113
                }
            };
            FindReplaceRequest test = new FindReplaceRequest();
            requests.Add(find);


            var response = request.Execute();

            // TODO: Change code below to process the `response` object:
            Console.WriteLine(JsonConvert.SerializeObject(response));
            return JsonConvert.SerializeObject(response);
        }

    }
}
