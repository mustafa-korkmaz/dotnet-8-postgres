﻿namespace Application.Dto
{
    public interface IDto<TKey>
    {
        TKey Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
    }

    public abstract class DtoBase<TKey> : IDto<TKey>
    {
        public TKey Id { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class ListDtoResponse<TDto> where TDto : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDto> Items { get; set; } = new List<TDto>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long RecordsTotal { get; set; }
    }

    public class ListDtoRequest
    {
        public bool IncludeRecordsTotal { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}