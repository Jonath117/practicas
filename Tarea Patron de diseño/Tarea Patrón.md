
## Patrón Singleton
### Patrón de tipo Creacional

El patrón Singleton nos permite asegurarnos tener una única instancia de una clase y al mismo que nos proporciona un punto de acceso global a la mencionada instancia.
Un ejemplo de su utilidad por ejemplo en una empresa, los empleados usan un sistema interno que se conecta a una base de datos central. Para evitar múltiples conexiones innecesarias y mantener la eficiencia, la aplicación tiene un Gestor de Configuración que maneja los parámetros de la conexión.

Si cada módulo de la aplicación creara su propia configuración, podrían surgir inconsistencias o conflictos. Para evitarlo, se usa el patrón Singleton, asegurando que solo haya una única instancia del gestor de configuración en todo el sistema.



## Diagrama del patrón singleton

![[diag_sing.png]]

## Pseudocódigo del ejemplo

Clase Configuracion:
    Variable privada estatica instancia <- NULO
    Variable privada estatica bloqueo <- nuevo Objeto()
    Variable privada cadenaConexion <- ""

    Metodo privado Constructor():
        cadenaConexion <- "Servidor=miServidor;BaseDatos=miDB;Usuario=admin;Contraseña=1234;"

    Metodo publico estatico ObtenerInstancia() devuelve Configuracion:
        Si instancia es NULO Entonces
            Bloquear acceso usando bloqueo:
                Si instancia es NULO Entonces
                    instancia <- nueva Configuracion()
        FinSi
        Retornar instancia

    Metodo publico ObtenerCadenaConexion() devuelve Cadena:
        Retornar cadenaConexion
FinClase

// Uso del Singleton en la aplicación
Inicio
    Configuracion config1 <- Configuracion.ObtenerInstancia()
    Configuracion config2 <- Configuracion.ObtenerInstancia()

    Mostrar "Cadena de conexión: " + config1.ObtenerCadenaConexion()

    Si config1 == config2 Entonces
        Mostrar "Ambas instancias son la misma."
    Sino
        Mostrar "Las instancias son diferentes (Error)."
    FinSi
Fin

## Relación con otros patrones 

Factory Method
Relación: Un Singleton puede usarse dentro de un Factory Method para garantizar que solo exista una única instancia del objeto creado.

Prototype
Relación: Mientras que Singleton evita múltiples instancias, Prototype permite crear copias de objetos. Se pueden combinar cuando necesitas una única instancia base (Singleton), pero quieres generar copias de sus datos sin modificar el original.

Facade
Relación: Facade simplifica el acceso a subsistemas complejos, y muchas veces usa Singleton para administrar instancias de servicios.

Independecy Injection
Relación: En aplicaciones modernas (como en ASP.NET Core), en lugar de usar Singleton directamente, se recomienda usar inyección de dependencias con AddSingleton() en el contenedor de servicios.

## Video que puede ayudar a la comprensión
https://youtu.be/gocJeOHtj9w?si=QtiTy5cKtfGaA2B6
https://youtube.com/shorts/xFkS6pC65YE?si=ybVMsCzoO0hynuR2


## Fuentes

**1.** Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1995). _Design Patterns: Elements of Reusable Object-Oriented Software_. Addison-Wesley Professional.

**2.** Freeman, E., Freeman, E., Bates, B., & Sierra, K. (2004). _Head First Design Patterns_. O'Reilly Media.