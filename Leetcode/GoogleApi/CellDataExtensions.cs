using Google.Apis.Sheets.v4.Data;
using System;

namespace GoogleApi
{
    public static class CellDataExtensions
    {
        public static string GetTextValue(this RowData rowData, int index) => rowData.Values?[index]?.UserEnteredValue?.StringValue;

        public static int GetIntValue(this RowData rowData, int index) => (int)(rowData.Values?[index].UserEnteredValue?.NumberValue ?? 0);

        public static double GetDoubleValue(this RowData rowData, int index) => (double)(rowData.Values?[index].UserEnteredValue?.NumberValue ?? 0);

        public static DateTime? GetDateTimeValue(this RowData rowData, int index)
        {
            if (rowData.Values == null || index >= rowData.Values.Count)
                return null;
            var numValue = rowData.Values[index]?.UserEnteredValue?.NumberValue;
            if (!numValue.HasValue)
                return null;
            return GoogleSpreadsheetClient.OriginDateTime.AddDays(numValue.Value);
        }
    }
}