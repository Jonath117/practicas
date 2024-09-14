using System.Security.Cryptography.X509Certificates;

//using System;
//using System.Text;

/*
Ventajas de las Listas Circulares
Iteración Continua: La principal ventaja es que puedes iterar sobre la lista tantas veces como sea necesario sin necesidad de reiniciar al llegar al final.
Eficiencia en Operaciones de Cola: En algunas implementaciones, como en colas de trabajo cíclicas, proporcionan una forma eficiente de manejar continuamente tareas que necesitan ejecución repetida.
Las listas circulares son especialmente útiles en aplicaciones que necesitan un acceso cíclico y continuo a los datos, como en la planificación de tareas, gestión de recursos en entornos de multitarea, o implementación de buffers de datos cíclicos.
*/


public class DoubleListNode<T>
{
    public T Data { get; set; }
    public DoubleListNode<T> Next { get; set; }
    public DoubleListNode<T> Previous { get; set; }

    public DoubleListNode(T data)
    {
        this.Data = data;
        this.Next = null;
        this.Previous = null;
    }
}

public class CircularDoubleLinkedList<T>
{
    private DoubleListNode<T> head; // Cabecera
    private DoubleListNode<T> tail; // Cola como cola de un animal, no cola, como la del supermercado

    public CircularDoubleLinkedList()
    {
        head = null;
        tail = null;
    }

    public void Add(T data)
    {
        DoubleListNode<T> newNode = new DoubleListNode<T>(data);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
            head.Next = head; 		// Apunta a sí mismo
            head.Previous = head; 	// Apunta a sí mismo
        }
        else
        {
            newNode.Previous = tail; 	// El anterior del nuevo nodo; ahora, es la cola
            tail.Next = newNode;		// La cola, ahora, apunta al nuevo nodo (el nuevo nodo, va al final)
            tail = newNode;				// En el Stack, el puntero a tail, se actualiza con la dirección del nuevo nodo
            tail.Next = head;			// El nuevo nodo, que ahora es tail, va a apuntar a la cabecera, por ser una lista circular
            head.Previous = tail;		// Por ser una lista circular, dobelmente enlazada, el anterior de la cabecera, es la cola
			
			/*
	        RAM (Stack)
	        head 00AA 
	        tail 00AC 


	        RAM (Heap)
	        00AA 1 Next 00AC
	        00AC 2 Next 00AA
	        ------------------------------

	        RAM (Stack)
	        head 00AA  Next 00AC

	        tail 00AC
				|
				V
	        tail 00AD	// Ahora, tail, apunta a nuevo. Ya no apunta a 00AC. NOTA.- Se cambia el valor del puntero, no se borra el objeto => tail = newNode;

	        newNode 00AD Next null // Inicialmente, apunta a null

	        RAM (Heap)
	        00AA 1 Next 00AC
	        00AC 2 Next 00AA => 00AD  ==> tail.Next = newNode
	        00AD 3 Next null => 00AA  ==> tail.Next = head;
	        ------------------------------

	        RAM (Stack)
	        head 00AA

	        tail 00AD
				|
				V
	        tail 00AE	// Ahora, tail, apunta a nuevo. Ya no apunta a 00AD. NOTA.- Se cambia el valor del puntero, no se borra el objeto => tail = newNode;

	        newNode 00AE Next null // Inicialmente, apunta a null

	        RAM (Heap)
	        00AA 1 Next 00AC
	        00AC 2 Next 00AD
	        00AD 3 Next 00AA => 00AE ==> tail.Next = newNode
	        00AE 4 Next null => 00AA ==> tail.Next = head;

	        ------------------------------
	        RAM (Stack)
	        head 00AA 

	        newNode 00AF 

	        tail 00AF 
				|
				V
	        tail 00AF // Ahora, tail, apunta a nuevo. Ya no apunta a 00AE. NOTA.- Se cambia el valor del puntero, no se borra el objeto => tail = newNode;


	        RAM (Heap)
	        00AA 1 Next 00AC
	        00AC 2 Next 00AD
	        00AD 3 Next 00AE
	        00AE 4 Next 00AA => 00AF ==> tail.Next = newNode
	        00AF 5 Next null => 00AA ==> tail.Next = head;


	        ------------------------------
	        RAM (Stack)
	        head 00AA  

	        newNode 00B0 

	        tail 00AF 
				|
				V
	        tail 00B0 // Ahora, tail, apunta a nuevo. Ya no apunta a 00AF. NOTA.- Se cambia el valor del puntero, no se borra el objeto


	        RAM (Heap)
	        00AA 1 Next 00AC
	        00AC 2 Next 00AD
	        00AD 3 Next 00AE
	        00AE 4 Next 00AF
	        00AF 5 Next 00AA => 00B0  ==> tail.Next = newNode
	        00B0 6 Next null => 00AC  ==> tail.Next = head;
	        */
        }
    }


	/*
	Objetivo.-
	Caso 1)
	RAM (Stack)
    head 00AA  
    tail 00B0


    RAM (Heap)
    00AA 1 Next 00AC :: Previous => 00B0
    00AC 2 Next 00AD :: Previous => 00AA
    00AD 3 Next 00AE :: Previous => 00AC
    00AE 4 Next 00AF :: Previous => 00AD
    00AF 5 Next 00B0 :: Previous => 00AE (X)
    00B0 6 Next 00AA :: Previous => 00AF
	-------------------------------------------------
	RAM (Stack)
    head 00AA  
    tail 00B0


    RAM (Heap)
    00AA 1 Next 00AC :: Previous => 00B0
    00AC 2 Next 00AD :: Previous => 00AA
    00AD 3 Next 00AE :: Previous => 00AC
    00AE 4 Next 00AF :: Previous => 00AD <==> Next 00B0 :: Previous => 00AD
    00AF 5 Next 00B0 :: Previous => 00AE (X)
		|
		V
    00B0 6 Next 00AA :: Previous => 00AF <==> Next 00AA :: Previous => 00AE
	=======================================================================================
	Caso 2)
	RAM (Stack)
    head 00AA  
    tail 00B0 


    RAM (Heap)
    00AA 1 Next 00AC :: Previous => 00B0
    00AC 2 Next 00AD :: Previous => 00AA
    00AD 3 Next 00AE :: Previous => 00AC
    00AE 4 Next 00AF :: Previous => 00AD
    00AF 5 Next 00B0 :: Previous => 00AE
    00B0 6 Next 00AA :: Previous => 00AF (x)
	-------------------------------------------------           
	RAM (Stack)
    head 00AA  
    tail 00B0 <==>  (00AF)
    

    RAM (Heap)
    00AA 1 Next 00AC :: Previous => 00B0
    00AC 2 Next 00AD :: Previous => 00AA
    00AD 3 Next 00AE :: Previous => 00AC
    00AE 4 Next 00AF :: Previous => 00AD 
    00AF 5 Next 00B0 :: Previous => 00AE <==> Next 00AA :: Previous => 00AF (nuevo tail)
    00B0 6 Next 00AA :: Previous => 00AF (X) 
	=======================================================================================
	Caso 3)
	RAM (Stack)
    head 00AA  
    tail 00B0 


    RAM (Heap)
    00AA 1 Next 00AC :: Previous => 00B0 (x)
    00AC 2 Next 00AD :: Previous => 00AA
    00AD 3 Next 00AE :: Previous => 00AC
    00AE 4 Next 00AF :: Previous => 00AD
    00AF 5 Next 00B0 :: Previous => 00AE
    00B0 6 Next 00AA :: Previous => 00AF 
	-------------------------------------------------
	RAM (Stack)
    head 00AA <==>  (00AC) 
    tail 00B0 


    RAM (Heap)
    00AA 1 Next 00AC :: Previous => 00B0 (X)
    00AC 2 Next 00AD :: Previous => 00AA <==> Next 00AD :: Previous => 00B0 (nuevo head)
    00AD 3 Next 00AE :: Previous => 00AC
    00AE 4 Next 00AF :: Previous => 00AD 
    00AF 5 Next 00B0 :: Previous => 00AE 
    00B0 6 Next 00AA :: Previous => 00AF <==> Next 00AC :: Previous => 00AF (nuevo head)
	*/
	public bool Delete(T data)
	{
        // lista vacía
	    if(head == null){
            return false;
        }

        bool deleted = false;

        DoubleListNode<T> current = head;

        do{
            
            if(current.Data.Equals(data)){
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;
                


                if(current == head && current == tail){
                    head = null;
                    tail = null;
                }

                else if(current == head){
                    head = current.Next;
                }
                
                else if(current == tail){
                    tail = current.Previous;
                }

                deleted = true;
                break;
            }

            current = current.Next;

            
        }
        while(current != head);

        return deleted;
	}

	
    public void Print()
    {
        if (head == null){
            System.Console.WriteLine("la lista esta vacia");
            return;
        }

        Console.WriteLine("Datos de la lista");
        DoubleListNode<T> current = head;
        do
        {
            if(current == null) break; // This is a safety check

            Console.Write(current.Data + " ");

            if(current.Next == null) break; // This is a safety check

            current = current.Next;

        } while (current != head); // Stop when we've come full circle
		
		Console.WriteLine();
		Console.WriteLine($"head.Data:{head.Data}, tail.Data:{tail.Data}");
    }
}

public class Program{
static void Main()

{
	CircularDoubleLinkedList<int> list = new CircularDoubleLinkedList<int>();
    
	list.Add(1);
	list.Add(2);
	list.Add(3);
    list.Add(4);

	list.Print(); // This will print 1, 2, 3 and then it could continue indefinitely
	
	// Test 1
	//Eliminar(list, 3);
	//Eliminar(list, 1);
	//Eliminar(list, 2);
		
	// Test 2
	//Eliminar(list, 1);
	//Eliminar(list, 2);
	//Eliminar(list, 3);
	
	// Test 3
	//Eliminar(list, 3);
	//Eliminar(list, 2);
	//Eliminar(list, 1);
	
	// Test 4
	//Eliminar(list, 2);
	//Eliminar(list, 3);
	//Eliminar(list, 1);
	
	// Test 5
	//Eliminar(list, 1);
	//Eliminar(list, 3);
	//Eliminar(list, 2);
	
	// Test 6
    Eliminar(list, 4);
	Eliminar(list, 1);
	Eliminar(list, 2);
	Eliminar(list, 3);
    
}

private static void Eliminar(CircularDoubleLinkedList<int> list, int key)
{
	Console.WriteLine($"Se va a eliminar elemento:{key}");
	if(!list.Delete(key)){
		Console.WriteLine($"No existe el elemento:{key}");
	}
	list.Print();
}
}
