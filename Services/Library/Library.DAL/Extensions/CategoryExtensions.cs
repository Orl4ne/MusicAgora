using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class CategoryExtensions
    {
        public static CategoryTO ToTransferObject (this CategoryEF Category)
        {
            if (Category is null)
                throw new ArgumentNullException(nameof(Category));

            return new CategoryTO
            {
                Id = Category.Id,
                Name = Category.Name,
            };
        }

        public static CategoryEF ToEF(this CategoryTO Category)
        {
            if (Category is null)
                throw new ArgumentNullException(nameof(Category));

            return new CategoryEF
            {
                Id = Category.Id,
                Name = Category.Name,
            };
        }

        public static CategoryEF ToTrackedEF(this CategoryTO Category, CategoryEF CategoryToModify)
        {
            if (CategoryToModify is null)
                throw new ArgumentNullException(nameof(CategoryToModify));
            if (Category is null)
                throw new ArgumentNullException(nameof(Category));

            CategoryToModify.Id = Category.Id;
            CategoryToModify.Name = Category.Name;

            return CategoryToModify;
        }

    }
}
