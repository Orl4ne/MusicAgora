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

        public static CategoryEF UpdateFromDetached(this CategoryEF AttachedEF, CategoryEF DetachedEF)
        {
            if (AttachedEF is null)
                throw new ArgumentNullException(nameof(AttachedEF));
            if (DetachedEF is null)
                throw new ArgumentNullException(nameof(DetachedEF));
            if ((AttachedEF != default) && (DetachedEF != default))
            {
                AttachedEF.Id = DetachedEF.Id;
                AttachedEF.Name = DetachedEF.Name;
            }
            return AttachedEF;
        }

    }
}
