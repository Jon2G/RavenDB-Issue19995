using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Maui_RavenDemo
{
    public class DocumentStoreHolder
    {
        // Use Lazy<IDocumentStore> to initialize the document store lazily.
        // This ensures that it is created only once - when first accessing the public `Store` property.
        private static readonly AsyncLazy<IDocumentStore> _Store = new AsyncLazy<IDocumentStore>(CreateStore);

        public static Task<IDocumentStore> StoreTask => _Store.Task;

        private static async Task<IDocumentStore> CreateStore()
        {
            byte[]? result = null;
            using var stream = await FileSystem.Current.OpenAppPackageFileAsync("free.jonyjouna.client.certificate.pfx");
            using var reader = new StreamReader(stream);
            using (MemoryStream ms = new MemoryStream())
            {
                stream?.CopyTo(ms);
                result = ms.ToArray();
            }
            IDocumentStore store = new DocumentStore()
            {
                // Define the cluster node URLs (required)
                Urls = new[]
                {
                    "https://a.free.jonyjouna.ravendb.cloud",
                    /*some additional nodes of this cluster*/
                },
                // Define a default database (optional)
                Database = "sample_db",
                // Define a client certificate (optional)
                Certificate = X509CertificateLoader.LoadPkcs12(data: result, password: null),

                // Initialize the Document Store
            }.Initialize();

            return store;
        }
    }
}
