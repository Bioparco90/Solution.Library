﻿namespace Model.Library
{
    // TODO: valutare utilità classe inutilizzata
    public class SearchHistoryParams
    {
        public string? Username { get; set; }
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorSurname { get; set; }
        public string? PublishingHouse { get; set; }
        public bool IsActive { get; set; }
    }
}