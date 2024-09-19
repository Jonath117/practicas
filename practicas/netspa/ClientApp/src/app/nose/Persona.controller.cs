using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Persona")]

public class PersonaController : ControllerBase
{
    private readonly IPersona _persona;

    public PersonaController(IPersona persona){
        _persona = persona;
    }
    
    [HttpGet("MostrarNombre")]
    public ActionResult MostrarNombre() 
    {
        string nombre = _persona.MostrarNombre();
        return Ok(nombre);
    }

    [HttpGet("MostrarEdad")]
    public ActionResult MostrarEdad()
    {
        int edad = _persona.MostrarEdad();
        return Ok(edad);
    }

    [HttpGet("MostrarDatos")]
    public ActionResult<object> MostrarDatos()
    {
        var datos = new {Nombre = _persona.MostrarNombre(), Edad = _persona.MostrarEdad()};
        return Ok(datos);
    }
}