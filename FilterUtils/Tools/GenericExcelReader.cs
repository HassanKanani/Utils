using Microsoft.AspNetCore.Http;
using Utils.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection;
namespace Utils.ExcelReader;

public static class GenericExcelReader<ClassType> where ClassType : class, new()
{
    public static async Task<ApiResponse<List<ClassType>>> GenericExcelReaderBaseic(IFormFile file, bool readByHeader = true, int StartRow = 0)
    {
        try
        {
            List<ClassType> list = new List<ClassType>();
            if (file == null || file.Length == 0)
            {
                return ApiResponse<List<ClassType>>.CreateErrorResponse("هیچ فایلی انتخاب نشده است.");
            }

            IWorkbook workbook;
            using (MemoryStream stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0L;
                if (file.FileName.EndsWith(".xlsx"))
                {
                    workbook = new XSSFWorkbook(stream, readOnly: false);
                }
                else
                {
                    if (!file.FileName.EndsWith(".xls"))
                    {
                        return ApiResponse<List<ClassType>>.CreateErrorResponse("فایل اکسل معتبر نیست.");
                    }

                    workbook = new HSSFWorkbook(stream);
                }
            }

            ISheet sheet = workbook.GetSheetAt(0);
            PropertyInfo[] properties = typeof(ClassType).GetProperties().ToArray();
            if (readByHeader)
            {
                IRow headerRow = sheet.GetRow(0);
                Dictionary<int, string> headerColumns = new Dictionary<int, string>();
                for (int col = 0; col < headerRow.LastCellNum; col++)
                {
                    string columnName2 = headerRow.GetCell(col)?.ToString();
                    if (!string.IsNullOrEmpty(columnName2))
                    {
                        headerColumns[col] = columnName2;
                    }
                }

                for (int j = StartRow; j <= sheet.LastRowNum; j++)
                {
                    IRow row2 = sheet.GetRow(j);
                    if (row2 == null)
                    {
                        continue;
                    }

                    ClassType excelDto2 = new ClassType();
                    for (int l = 0; l < row2.LastCellNum; l++)
                    {
                        string cellValue2 = row2.GetCell(l)?.ToString();
                        if (headerColumns.TryGetValue(l, out string columnName))
                        {
                            PropertyInfo property2 = properties.FirstOrDefault((PropertyInfo p) => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
                            if (property2 != null)
                            {
                                SetPropertyValue(property2, excelDto2, cellValue2);
                            }
                        }
                    }

                    list.Add(excelDto2);
                }
            }
            else
            {
                for (int i = StartRow; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row != null)
                    {
                        ClassType excelDto = new ClassType();
                        for (int k = 0; k < properties.Length && k < row.LastCellNum; k++)
                        {
                            string cellValue = row.GetCell(k)?.ToString();
                            PropertyInfo property = properties[k];
                            SetPropertyValue(property, excelDto, cellValue);
                        }

                        list.Add(excelDto);
                    }
                }
            }

            return ApiResponse<List<ClassType>>.CreateSuccessResponse(list, "فایل پردازش شد.");
        }
        catch (Exception ex2)
        {
            Exception ex = ex2;
            return ApiResponse<List<ClassType>>.CreateErrorResponse("خطا در پردازش فایل: " + ex.Message);
        }
    }

    private static void SetPropertyValue(PropertyInfo property, ClassType obj, string cellValue)
    {
        if (property.PropertyType == typeof(int))
        {
            if (int.TryParse(cellValue, out var result))
            {
                property.SetValue(obj, result);
            }
        }
        else if (property.PropertyType == typeof(byte))
        {
            if (byte.TryParse(cellValue, out var result2))
            {
                property.SetValue(obj, result2);
            }
        }
        else
        {
            property.SetValue(obj, cellValue);
        }
    }
}
