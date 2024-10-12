using Microsoft.AspNetCore.Http;
using Utils.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.ComponentModel.DataAnnotations;
namespace Utils.ExcelReader;

public class GenericExcelReader<ClassType> where ClassType : class, new()
{
    public async Task<ApiResponse<List<ClassType>>> GenericExcelReaderBaseic(IFormFile file)
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

        for (int i = 0; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            var rows = new List<string>();

            var properties = typeof(ClassType).GetProperties().Where(p=>!p.GetCustomAttributes(typeof(KeyAttribute),true).Any()).ToArray();
            if (row != null)
            {
                ClassType excelDto = new ClassType();
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
}
