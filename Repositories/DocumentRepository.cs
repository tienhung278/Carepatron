using System;
using System.Reflection.Metadata;
using api.Data;
using api.Repositories.Contracts;
using api.Utilities;

namespace api.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _dataContext;

        public DocumentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task SyncDocumentsFromExternalSource(string _)
        {
            // simulates random errors that occur with external services
            // leave this to emulate real life
            ChaosUtility.RollTheDice();

            // this simulates sending an email
            // leave this delay as 10s to emulate real life
            await Task.Delay(10000);
        }
    }
}

