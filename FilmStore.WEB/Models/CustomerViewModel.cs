using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FilmStore.WEB.Models
{
  public class CustomerViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public string UserRef { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public UserViewModel User { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public List<PurchaseViewModel> Purchases { get; set; }
  }
}
