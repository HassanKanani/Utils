using Microsoft.AspNetCore.Http;
using Utils.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace Utils.ExcelReader;

public static class GenericExcelReader<ClassType> where ClassType : class, new()
{
    public static async Task<ApiResponse<List<ClassType>>> GenericExcelReaderBaseic(IFormFile file)
    {
        try
        {
            List<ClassType> list = new();
            if (file == null || file.Length == 0)
                return ApiResponse<List<ClassType>>.CreateErrorResponse("هیچ فایلی انتخاب نشده است.");

            IWorkbook workbook;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                if (file.FileName.EndsWith(".xlsx"))
                {
                    workbook = new XSSFWorkbook(stream);
                }
                else if (file.FileName.EndsWith(".xls"))
                {
                    workbook = new HSSFWorkbook(stream);
                }
                else
                {
                    return ApiResponse<List<ClassType>>.CreateErrorResponse("فایل اکسل معتبر نیست.");
                }
            }

            ISheet sheet = workbook.GetSheetAt(0);

            // خواندن نام ستون‌ها از خط اول (Header)
            IRow headerRow = sheet.GetRow(0);
            var headerColumns = new Dictionary<int, string>();
            for (int col = 0; col < headerRow.LastCellNum; col++)
            {
                var columnName = headerRow.GetCell(col)?.ToString();
                if (!string.IsNullOrEmpty(columnName))
                {
                    headerColumns[col] = columnName;
                }
            }

            // خواندن داده‌ها از خط‌های بعدی
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    ClassType excelDto = new();
                    var properties = typeof(ClassType).GetProperties();

                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cellValue = row.GetCell(j)?.ToString();

                        // پیدا کردن پراپرتی مرتبط با ستون
                        if (headerColumns.TryGetValue(j, out string columnName))
                        {
                            var property = properties.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
                            if (property != null)
                            {
                                if (property.PropertyType == typeof(int))
                                {
                                    if (int.TryParse(cellValue, out int intValue))
                                    {
                                        property.SetValue(excelDto, intValue);
                                    }
                                }
                                else if (property.PropertyType == typeof(byte))
                                {
                                    if (byte.TryParse(cellValue, out byte byteValue))
                                    {
                                        property.SetValue(excelDto, byteValue);
                                    }
                                }
                                else
                                {
                                    property.SetValue(excelDto, cellValue);
                                }
                            }
                        }
                    }
                    list.Add(excelDto);
                }
            }

            return ApiResponse<List<ClassType>>.CreateSuccessResponse(list, "فایل پردازش شد.");
        }
        catch (Exception ex)
        {
            return ApiResponse<List<ClassType>>.CreateErrorResponse($"خطا در پردازش فایل: {ex.Message}");
        }
    }

    public static async Task<ApiResponse<List<ClassType>>> GenericExcelReaderBaseic(string filePath)
    {
        List<ClassType> list = new();

        // بررسی وجود فایل
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            return ApiResponse<List<ClassType>>.CreateErrorResponse("فایل انتخاب شده وجود ندارد.");

        IWorkbook workbook;
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            if (filePath.EndsWith(".xlsx"))
            {
                workbook = new XSSFWorkbook(stream);
            }
            else if (filePath.EndsWith(".xls"))
            {
                workbook = new HSSFWorkbook(stream);
            }
            else
            {
                return ApiResponse<List<ClassType>>.CreateErrorResponse("فایل اکسل معتبر نیست.");
            }
        }

        ISheet sheet = workbook.GetSheetAt(0);

        for (int i = 0; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            if (row != null)
            {
                ClassType excelDto = new ClassType();
                var properties = typeof(ClassType).GetProperties().Where(v => v.Name.ToLower() != "id").ToArray();

                for (int j = 0; j < properties.Length && j < row.LastCellNum; j++)
                {
                    var cellValue = row.GetCell(j)?.ToString();
                    var property = properties[j];

                    if (property.PropertyType == typeof(int))
                    {
                        if (int.TryParse(cellValue, out int intValue))
                        {
                            property.SetValue(excelDto, intValue);
                        }
                    }
                    else
                    {
                        property.SetValue(excelDto, cellValue);
                    }
                }

                list.Add(excelDto);
            }
        }

        return ApiResponse<List<ClassType>>.CreateSuccessResponse(list, "فایل پردازش شد.");
    }
    public static async Task<string> SaveFileAndReturnPath(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0) throw new Exception("هیچ فایلی انتخاب نشده است.");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "files");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filepath = Path.Combine(path, DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + System.IO.Path.GetExtension(file.FileName));
            using (var stream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);

            }
            return filepath;
        }
        catch (Exception)
        {

            throw;
        }


    }
}
