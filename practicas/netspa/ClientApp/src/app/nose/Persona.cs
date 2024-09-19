public class Persona : IPersona
{
    public string nombre;
    public int edad;

    public Persona(string nombre, int edad){
        this.nombre = "JonathanVerstappen";
        this.edad = 19;
    }
    
  public string MostrarNombre()
  {
    return nombre;
  }
  public int MostrarEdad()
  {
    return edad;
  }

}