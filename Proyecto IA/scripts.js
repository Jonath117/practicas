const estadoTexto = document.getElementById("estado");
const mensajeTexto = document.getElementById("mensaje");
const emojiContenedor = document.getElementById("emoji");

let model;

async function loadModel() {
    try {
        model = await tmImage.load('./Imagen_insertada/model.json', './Imagen_insertada/metadata.json');
        console.log("Modelo cargado");
    } catch (error) {
        console.error("Error al cargar el modelo:", error);
    }
}

async function startScreenCapture() {
    try {
        const stream = await navigator.mediaDevices.getDisplayMedia({ video: true });
        const video = document.createElement('video');
        video.srcObject = stream;
        video.play();
        return video;
    } catch (err) {
        console.error("Error al capturar la pantalla:", err);
    }
}

async function detectarEstado(video) {
    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d');
    canvas.width = 640;
    canvas.height = 480;

    async function procesar() {
        ctx.drawImage(video, 0, 0, canvas.width, canvas.height);
        const prediction = await model.predict(canvas);

        if (prediction && prediction.length > 0) {
            let maxPrediction = prediction.reduce((prev, current) =>
                prev.probability > current.probability ? prev : current
            );

            const estado = maxPrediction.className;
            estadoTexto.textContent = `Estado: ${estado}`;

            // Mostrar mensaje, emoji y color de fondo según emoción
            switch (estado) {
                case "Feliz":
                    mensajeTexto.textContent = "😊 ¡Qué bueno verte feliz! Sigue así.";
                    emojiContenedor.textContent = "😊";
                    document.body.style.backgroundColor = "#D4EDDA"; // verde claro
                    break;
                case "Triste":
                    mensajeTexto.textContent = "😢 Parece que estás triste. ¿Qué tal si te tomas un descanso o hablas con alguien?";
                    emojiContenedor.textContent = "😢";
                    document.body.style.backgroundColor = "#D6E9F9"; // azul claro
                    break;
                case "Enojado":
                    mensajeTexto.textContent = "😠 Respira hondo, quizás te ayude a relajarte un poco.";
                    emojiContenedor.textContent = "😠";
                    document.body.style.backgroundColor = "#F8D7DA"; // rojo claro
                    break;
                case "Cansado":
                    mensajeTexto.textContent = "😴 Te ves cansado, considera tomar una pausa o beber agua.";
                    emojiContenedor.textContent = "😴";
                    document.body.style.backgroundColor = "#FFF3CD"; // amarillo claro
                    break;
                case "Serio":
                    mensajeTexto.textContent = "😐 Te ves concentrado.";
                    emojiContenedor.textContent = "😐";
                    document.body.style.backgroundColor = "#E2E3E5"; // gris claro
                    break;
                default:
                    mensajeTexto.textContent = "";
                    emojiContenedor.textContent = "";
                    document.body.style.backgroundColor = "white";
            }
        } else {
            console.log("No se pudo predecir correctamente.");
        }

        setTimeout(procesar, 1000); // Ejecuta el procesamiento cada segundo
    }

    procesar();
}

async function iniciar() {
    await loadModel();
    const video = await startScreenCapture();
    detectarEstado(video);
}

//python -m http.server 8000
//http://localhost:8000/index.html
