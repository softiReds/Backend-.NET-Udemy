﻿var sale = new Sale(15);
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

var numbers = new MyList<int>(5);
var names = new MyList<string>(5);
var beers = new MyList<Beer>(2);

beers.Add(new Beer() { Name = "CERVEZA 1", Price = 10 });
beers.Add(new Beer() { Name = "CERVEZA 2", Price = 20 });

Console.WriteLine(numbers.GetContent());
Console.WriteLine(names.GetContent());
Console.WriteLine(beers.GetContent());

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
    public string Name {  get; set; }
    public int Price {  get; set; }

    public void Save()
    {
        Console.WriteLine("Se guardó un sevicio");
    }

    public override string ToString()
    {
        return $"Name: {Name} Price: {Price}";
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

//  GENERICS
public class MyList<T>
{
    private List<T> _list;
    private int _limit;

    public MyList(int limit)
    {
        _limit = limit;
        _list = new List<T>();
    }

    public void Add(T item)
    {
        if(_list.Count < _limit)
        {
            _list.Add(item);
        }
    }

    public string GetContent()
    {
        string content = "";
        foreach(var element in _list)
        {
            content += element + " ";
        }

        return content;
    }
}