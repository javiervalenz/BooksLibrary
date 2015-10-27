using BooksLibrary.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.DomainModel.Repositories
{
    public sealed class DbContext
    {
        private static readonly Lazy<DbContext> instance = new Lazy<DbContext>(() => new DbContext());
        private BooksLibraryEntities context;

        private DbContext()
        {
            BooksLibraryEntities context = new BooksLibraryEntities();
        }        

        public static DbContext Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public BooksLibraryEntities Context
        {
            get { return context; }
        }

    }
}
