using System.Collections.Generic;

public class Country
{
    // Gets or sets the Name
    public string Name { get; set; }

    // Gets or sets the Top Level Domain
    public IList<string> TopLevelDomain { get; set; }

    // Gets or sets the Alpha2 Code
    public string Alpha2Code { get; set; }

    // Gets or sets the Alpha3 Code
    public string Alpha3Code { get; set; }

    // Gets or sets the Calling Codes
    public IList<string> CallingCodes { get; set; }

    // Gets or sets the Capital City
    public string Capital { get; set; }

    // Gets or sets the Alt Spellings
    public IList<string> AltSpellings { get; set; }

    // Gets or sets the Region
    public string Region { get; set; }

    // Gets or sets the Subregion
    public string Subregion { get; set; }

    // Gets or sets the Population
    public int Population { get; set; }

    // Gets or sets the Latlng(Latitude and Longitude)
    public IList<double> Latlng { get; set; }

    // Gets or sets the Demonym
    public string Demonym { get; set; }

    // Gets or sets the Area
    public double? Area { get; set; }

    // Gets or sets the Gini
    public double? Gini { get; set; }

    // Gets or sets the Timezones
    public IList<string> Timezones { get; set; }

    // Gets or sets the Borders
    public IList<string> Borders { get; set; }

    // Gets or sets the Native Name
    public string NativeName { get; set; }

    // Gets or sets the Numeric Code
    public string NumericCode { get; set; }

    // Gets or sets the Cioc(International Olympic Committee Code)
    public string Cioc { get; set; }
}