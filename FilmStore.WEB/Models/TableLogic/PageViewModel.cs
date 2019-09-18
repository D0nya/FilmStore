﻿using System;

namespace FilmStore.WEB.Models.TableLogic
{
  public class PageViewModel
  {
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }

    public PageViewModel(int count, int pageNumber, int pageSize)
    {
      PageNumber = pageNumber;
      TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public bool HasPreviousPage
    {
      get { return PageNumber > 1; }
    }
    public bool HasNextPage
    {
      get { return PageNumber < TotalPages; }
    }
  }
}
