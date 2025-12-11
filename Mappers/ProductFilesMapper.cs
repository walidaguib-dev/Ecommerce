using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class ProductFilesMapper
    {
        public static ProductFiles ToModel(this CreateProductFile dto)
        {
            return new ProductFiles
            {
                ProductId = dto.ProductId,
                fileId = dto.FileId
            };
        }

        public static ProductFilesDto ToDto(this ProductFiles productFiles)
        {
            return new ProductFilesDto
            {
                FileId = productFiles.fileId,
                fileName = productFiles.file.file,
                ProductId = productFiles.ProductId,
                Product_name = productFiles.Product.Name
            };
        }
    }
}