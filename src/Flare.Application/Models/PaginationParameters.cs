﻿namespace Flare.Application.Models;

public abstract class PaginationParameters
{
    public int Page { get; set; } = 1;
    const int MaxPageSize = 30;

    private int _pageSize = 20;
    public virtual int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

    }
}