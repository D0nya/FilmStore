using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmStore.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace FilmStore.WEB.Models
{
  public class FilmViewModel
  {
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }
    [Display(Name = "Name")]
    public string Name { get; set; }
    [HiddenInput(DisplayValue = false)]
    public int QuantityInStock { get; set; }
    [Display(Name = "Year")]
    public string Year { get; set; }
    [Display(Name = "Price")]
    public decimal Price { get; set; }
    [Display(Name = "Rate")]
    public float Rate { get; set; }
    public string ImagePath { get; set; }
    [Display(Name = "Film status")]
    public FilmStatus Status { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int ProducerId { get; set; }
    public ProducerViewModel Producer { get; set; }

    [HiddenInput(DisplayValue = false)]
    public List<int> CountriesId { get; set; }
    public List<CountryViewModel> Countries { get; set; }

    [HiddenInput(DisplayValue = false)]
    public List<int> GenresId { get; set; }
    public List<GenreViewModel> Genres { get; set; }

    [HiddenInput(DisplayValue = false)]
    public List<PurchaseViewModel> Purchases { get; set; }
    public IFormFile Image { get; set; }
  }
}
