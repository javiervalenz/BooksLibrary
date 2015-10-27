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
        //private static readonly Lazy<DbContext> instance = new Lazy<DbContext>(() => new DbContext());
        private static DbContext instance;
        private BooksLibraryEntities context;

        private DbContext()
        {
            context = new BooksLibraryEntities();
        }        

        public static DbContext Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DbContext();
                }                    

                return instance;
            }
        }

        public BooksLibraryEntities Context
        {
            get { return context; }
        }

    }
}
