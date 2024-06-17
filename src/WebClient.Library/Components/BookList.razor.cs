using Microsoft.AspNetCore.Components;
using Model.Library;

namespace WebClient.Library.Components
{
    public partial class BookList : ComponentBase
    {
        List<Book> books = [];

        protected override void OnInitialized()
        {
            books = BookHandler.GetAll().ToList();
        }

    }
}
