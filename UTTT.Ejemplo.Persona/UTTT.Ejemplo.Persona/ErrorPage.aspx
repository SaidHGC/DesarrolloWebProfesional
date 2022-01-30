<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="UTTT.Ejemplo.Persona.ErrorPage" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Error</title>
</head>
<body>
    <div class="container">
        <b><h2>A OCURRIDO UN ERROR</h2></b>
        
        <div id="foolContainer">
            <img id="fool" src="Images/fool.jpg" alt="">
        </div>
        <div id="divError">
            <b><p id="errorText">Existe un problema en la aplicación, favor de volver a la ventana anterior e intentar nuevamente. 
                <br> Cuidado con las acciones que realiza</p></b>
        </div>
           
        <table id="imgs">
            <tr>
                <td id="invisible"><img id="shinso" src="Images/Shinso.jpg" alt="Shinso"></td>
                <td><img id="shinso" src="Images/Shinso.jpg" alt="Shinso"></td>
                <td id="invisible"><img id="shinso" src="Images/Shinso.jpg" alt="Shinso"></td>
                <img id="mago" src="Images/Mago.jpg" alt="">
            </tr>
        </table>
    </div>
</body>
</html>

<style>
    @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;300&display=swap');
    html{
        margin: 0;
        padding: 0;
        font-family: 'Montserrat', sans-serif;
        background-image: url("Images/fondo.jpg");
        background-repeat: no-repeat;
        background-size: cover;
    }

    h2{
        font-family:-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
        font-size: 60px;
        text-align: center;
        color: #FFD180;
    }

    #imgs{
        margin-top: 20%;
        z-index: 9;
        text-align: center;
    }

    #invisible{
        visibility: hidden;
    }

    #shinso{
        width: 100%;
    }

    #divError{
        position: absolute;
        z-index: 10;
    }

    #errorText{
        z-index: 10;
        color: cornsilk;
        margin: 5%;
        text-align: center;
        font-size: 28px;
    }

    #foolContainer{
        position: absolute;
        z-index: 0;
    }

    #fool{
        left: 10%;
        float: right;
        position: relative;
        z-index: 0;
        width: 17%;
        transform: rotate(315deg);
        opacity: 0.7;
    }

    #mago{
        margin-top: 20%;
        position: absolute;
        z-index: 0;
        width: 15%;
        transform: rotate(45deg);
        opacity: 0.9;
    }
</style>