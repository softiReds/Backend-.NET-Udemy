var sale = new Sale(15);
//  Sale sale = new Sale();
//  Sale sale = new();
var message = sale.GetInfo();
Console.WriteLine(message);

var saleTax = new SaleWithTax(15, 1.16m);
var messageTax = saleTax.GetInfo();
Console.WriteLine(messageTax);

var beer = new Beer();

Some(sale);
Some(beer);

void Some(ISave save)
{
    save.Save();
}

class SaleWithTax : Sale
{
    public decimal Tax {  get; set; }

    public SaleWithTax(decimal total, decimal tax) : base(total)
    {
        Tax = tax;
    }

    public override string GetInfo()
    {
        return $"El total es {Total} Impuesto es: {Tax}";
    }

    public string GetInfo(string message)
    {
        return message;
    }
}

class Sale  : ISale, ISave
{
    public decimal Total { get; set; }

    public Sale(decimal total)
    {
        Total = total;
    }

    public virtual string GetInfo()    //   virtual -> Indica que el metodo puede ser sobreescrito
    {
        return $"El total es {Total}";
    }

    public void Save()
    {
        Console.WriteLine("Se guardó en BD");
    }
}

public class Beer : ISave
{
    public void Save()
    {
        Console.WriteLine("Se guardó un sevicio");
    }
}

interface ISale
{
    decimal Total { get; set; }
}

interface ISave
{
    public void Save();
}