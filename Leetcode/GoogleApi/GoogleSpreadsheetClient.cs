using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleApi
{
    public class GoogleSpreadsheetClient
    {
        internal static readonly DateTime OriginDateTime = new DateTime(1899, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc);

        private readonly ClientSecrets _clientSecrets;
        private readonly string _spreadsheetId;

        public GoogleSpreadsheetClient(GoogleSpreadsheetClientConfiguration config)
        {
            _clientSecrets = new ClientSecrets
            {
                ClientId = config.ClientId,
                ClientSecret = config.ClientSecret
            };

            _spreadsheetId = config.SpreadsheetId;
        }

        public async Task<CompanySheet> LoadCompanySheet(string company)
        {
            using var service = await CreateSheetsServiceAsync();

            var request = service.Spreadsheets.Get(_spreadsheetId);
            request.IncludeGridData = true;
            var response = await request.ExecuteAsync();

            var companySheet = response
                .Sheets
                .FirstOrDefault(s => s.Properties.Title.Contains(company, StringComparison.OrdinalIgnoreCase));

            if (companySheet == null)
            {
                var availableCompanyNames = string.Join(", ", response.Sheets.Select(s => s.Properties.Title));
                throw new ArgumentException($"Company name {company} wasn't found in spreadsheet tabs. Available names are: {availableCompanyNames}.");
            }

            return new CompanySheet
            {
                Title = companySheet.Properties.Title,
                Index = companySheet.Properties.Index,
                SheetId = companySheet.Properties.SheetId,
                Questions = MapRowDataToSpreadsheetQuestions(companySheet.Data[0].RowData)
            };
        }

        private static List<SpreadsheetQuestion> MapRowDataToSpreadsheetQuestions(IList<RowData> rowData)
        {
            if (rowData == null)
                return new List<SpreadsheetQuestion>();

            var rowNumber = 1;
            return rowData
                .Skip(1)
                .Where(x => x.Values?.Count > 1)
                .Select(x =>
                    new SpreadsheetQuestion
                    {
                        RowNumber = rowNumber++,
                        Id = x.GetTextValue(0),
                        FrontendId = x.GetTextValue(1),
                        Title = x.GetTextValue(2),
                        Difficulty = x.GetTextValue(3),
                        Status = x.GetTextValue(4),
                        Frequency6Months = x.GetIntValue(5),
                        Frequency1Year = x.GetIntValue(6),
                        Frequency2Years = x.GetIntValue(7),
                        FrequencyAllTime = x.GetIntValue(8),
                        CalculatedFrequency6Months = x.GetDoubleValue(9),
                        CalculatedFrequency1Year = x.GetDoubleValue(10),
                        CalculatedFrequency2Years = x.GetDoubleValue(11),
                        CalculatedFrequencyAllTime = x.GetDoubleValue(12),
                        Slug = x.GetTextValue(13),
                        Tags = x.GetTextValue(14),
                        AddedDateTime = x.GetDateTimeValue(15),
                        LastSubmittedDateTime = x.GetDateTimeValue(16)
                    })
                .Where(x => !string.IsNullOrEmpty(x.Id))
                .ToList();
        }

        public async Task<List<SpreadsheetQuestion>> LoadQuestionsAsync(int? companySheetIndex = null)
        {
            using var service = await CreateSheetsServiceAsync();

            var request = service.Spreadsheets.Get(_spreadsheetId);
            request.IncludeGridData = true;

            var response = await request.ExecuteAsync();
            var sheetIndex = companySheetIndex ?? 0;

            var rowData = response.Sheets[sheetIndex].Data[0].RowData;

            return MapRowDataToSpreadsheetQuestions(rowData);
        }

        public async Task AddQuestionsAsync(IList<SpreadsheetQuestion> questions, int? sheetId = null)
        {
            if (!questions.Any())
                return;

            using var service = await CreateSheetsServiceAsync();

            var appendCellsRequest = new AppendCellsRequest
            {
                Rows = questions.Select(QuestionToRowData).ToList(),
                Fields = "*",
                SheetId = sheetId
            };
            var request = new Request { AppendCells = appendCellsRequest };
            await SendRequestAsync(service, request);
        }

        public async Task UpdateQuestionsAsync(IList<SpreadsheetQuestion> questions, int? sheetId = null)
        {
            using var service = await CreateSheetsServiceAsync();

            var appendCellsRequest = new UpdateCellsRequest
            {
                Rows = questions.Select(QuestionToRowData).ToList(),
                Fields = "*",
                Start = new GridCoordinate
                {
                    ColumnIndex = 0,
                    RowIndex = 1,
                    SheetId = sheetId
                }
            };
            var request = new Request { UpdateCells = appendCellsRequest };
            await SendRequestAsync(service, request);
        }

        public async Task UpdateLastSubmittedDateTime(int rowIndex, DateTime lastSubmittedDateTime, int? sheetId = null)
        {
            using var service = await CreateSheetsServiceAsync();

            var cells = new List<CellData>();
            AddDateTimeCell(cells, lastSubmittedDateTime);
            var appendCellsRequest = new UpdateCellsRequest
            {
                Rows = new List<RowData>
                {
                    new RowData { Values = cells }
                },
                Fields = "*",
                Start = new GridCoordinate
                {
                    ColumnIndex = 16,
                    RowIndex = rowIndex,
                    SheetId = sheetId
                }
            };
            var request = new Request { UpdateCells = appendCellsRequest };
            await SendRequestAsync(service, request);
        }

        public async Task SortRange()
        {
            using var service = await CreateSheetsServiceAsync();

            var sortRequest = new SortRangeRequest
            {
                Range = new GridRange
                {
                    StartRowIndex = 1,
                    EndRowIndex = int.MaxValue,
                    StartColumnIndex = 0,
                    EndColumnIndex = int.MaxValue
                },
                SortSpecs = new List<SortSpec>
                {
                    new SortSpec { DimensionIndex = 15, SortOrder = "ASCENDING" },
                    new SortSpec { DimensionIndex = 5, SortOrder = "DESCENDING" },
                }
            };

            var request = new Request { SortRange = sortRequest };
            await SendRequestAsync(service, request);
        }

        private static RowData QuestionToRowData(SpreadsheetQuestion question)
        {
            var cells = new List<CellData>();
            AddTextCell(cells, question.Id);
            AddTextCell(cells, question.FrontendId);
            AddTextCell(cells, question.Title);
            AddTextCell(cells, question.Difficulty);
            AddTextCell(cells, question.Status);
            AddNumericCell(cells, question.Frequency6Months);
            AddNumericCell(cells, question.Frequency1Year);
            AddNumericCell(cells, question.Frequency2Years);
            AddNumericCell(cells, question.FrequencyAllTime);
            AddNumericCell(cells, question.CalculatedFrequency6Months);
            AddNumericCell(cells, question.CalculatedFrequency1Year);
            AddNumericCell(cells, question.CalculatedFrequency2Years);
            AddNumericCell(cells, question.CalculatedFrequencyAllTime);
            AddFormulaCell(cells, question.Slug);
            AddTextCell(cells, question.Tags);
            if (question.AddedDateTime.HasValue)
                AddDateTimeCell(cells, question.AddedDateTime.Value);
            return new RowData { Values = cells };
        }

        private static void AddTextCell(List<CellData> cells, string value)
        {
            var cellData = new CellData { UserEnteredValue = new ExtendedValue { StringValue = value } };
            cells.Add(cellData);
        }

        private static void AddNumericCell(List<CellData> cells, double value)
        {
            var cellData = new CellData { UserEnteredValue = new ExtendedValue { NumberValue = value } };
            cells.Add(cellData);
        }

        private static void AddDateTimeCell(List<CellData> cells, DateTime dateTime)
        {
            var diff = (dateTime - OriginDateTime).TotalDays;
            var cellData = new CellData
            {
                UserEnteredValue = new ExtendedValue { NumberValue = diff },
                UserEnteredFormat = new CellFormat { NumberFormat = new NumberFormat { Type = "DATE" } }
            };
            cells.Add(cellData);
        }

        private static void AddFormulaCell(List<CellData> cells, string value)
        {
            var cellData = new CellData { UserEnteredValue = new ExtendedValue { FormulaValue = value } };
            cells.Add(cellData);
        }

        private async Task<SheetsService> CreateSheetsServiceAsync()
        {
            var scopes = new[] { SheetsService.Scope.Spreadsheets };

            var dataStore = new FileDataStore("token.json", true);

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                _clientSecrets,
                scopes,
                "user",
                CancellationToken.None,
                dataStore);

            var initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name
            };

            return new SheetsService(initializer);
        }

        private async Task SendRequestAsync(SheetsService service, Request request)
        {
            var batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest { Requests = new[] { request } };
            var batchUpdateRequest = service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, _spreadsheetId);
            await batchUpdateRequest.ExecuteAsync();
        }

        public async Task<IEnumerable<string>> GetSheetNames()
        {
            using var service = await CreateSheetsServiceAsync();

            var request = service.Spreadsheets.Get(_spreadsheetId);

            //request.IncludeGridData = true;

            var response = await request.ExecuteAsync();
            return response.Sheets.Select(s => s.Properties.Title);
        }
    }
}