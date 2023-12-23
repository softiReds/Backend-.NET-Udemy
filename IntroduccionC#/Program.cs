var sale = new Sale(15);
//  Sale sale = new Sale();
//  Sale sale = new();
var message = sale.GetInfo();
Console.WriteLine(message);

var saleTax = new SaleWithTax(15, 1.16m);
var messageTax = saleTax.GetInfo();
Console.WriteLine(messageTax);

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

class Sale
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
}