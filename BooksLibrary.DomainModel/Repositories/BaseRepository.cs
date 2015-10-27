using BooksLibrary.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.DomainModel.Repositories
{
    public class BaseRepository : IDisposable
    {
        protected BooksLibraryEntities dbContext;
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool d)
        {
            if (!this.disposed)
            {
                if (d)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
