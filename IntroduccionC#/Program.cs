using System.Text.Json;

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

var numbers = new MyList<int>(5);
var names = new MyList<string>(5);
var beers = new MyList<Beer>(2);

beers.Add(new Beer() { Name = "CERVEZA 1", Price = 10 });
beers.Add(new Beer() { Name = "CERVEZA 2", Price = 20 });

Console.WriteLine(numbers.GetContent());
Console.WriteLine(names.GetContent());
Console.WriteLine(beers.GetContent());

var santiago = new People()
{
    Name = "Santiago",
    Age = 18
};

string json = JsonSerializer.Serialize(santiago);   //  Serializer(object) -> Convierte un objeto en JSON
Console.WriteLine(json);

string myJson = @"{""Name"":""Juan"",""Age"":18}";

var juan = JsonSerializer.Deserialize<People>(myJson);  //  Deserialize<class>(json) -> Convierte un JSON en un objeto de tipo class
Console.WriteLine(juan.Name);
Console.WriteLine(juan.Age);

//  PROGRAMACIÓN FUNCIONAL
//  Funcion pura -> Funcion que retorna el mismo valor (en base a sus parametros) y no altera nada de su exterior. Los parametros que recibe siempre son pasados por valor (no por referencia)
int Suma(int a, int b) => a + b;

//  Funcion de primera clase -> Funcion que se puede guardar en una variable
void Show(string message) => Console.WriteLine(message);

var show = Show;
show("Hola");

SomeA(show, "Hola, ¿Cómo estás?");

void SomeA(Action<string> fn, string message)   //  Action<type> name -> Hace alución a una funcion que no retorna nada, cuando se incluye un type se está indicando que la funcion recibe un parametro (y por lo tanto debemos recibir ese parametro para poder enviarselo en el momento que queramos utilizar la funcion dentro de la funcion "padre")
{
    Console.WriteLine("Hace algo aquí");

    fn(message);    //  Ejecucion de la funcion recibida por parametro

    Console.WriteLine("Hace algo al final");
}
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

//  JSON
public class People
{
    public string Name { get; set; }
    public int Age {  get; set; }
}