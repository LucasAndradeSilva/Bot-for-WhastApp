// Supports ES6
// import { create, Whatsapp } from 'venom-bot';
const venom = require('venom-bot');
const axios = require('axios');


venom.create().then((client) => start(client)).catch((erro) => {
    console.log(erro);
});

function start(client) {       
    client.onMessage(async (message) => {     

        console.log({
            Nunmber: message.from,
            Name: message.sender.pushname,
            Message: message.body
            //Stage: banco.db[message.from].stage         
        });

        if(message.from === "5511963118371@c.us"){     
            //Pegar número de Telefone e conferir o stado desse cliente 
            var stage = await getStage(message.from, message.sender.pushname)

            
            if(stage === 1){
                client.sendText(message.from, `Oi *${message.sender.pushname}*, eu sou o *Du*, o assitente virtual da Visão grupo!😀`);   
                client.sendText(message.from, "Escolha umas das opções a baixo para continuar:👇 \n\n 1 - Solicitar Token de Acesso \n 2 - Falar com o Suporte \n 3 - Comprar produtos");   
                updateStage(message.from);
            }
            else if(stage === 2){
                if (message.body == "1") {
                    var opacao = await getOpcao(message.from, message.body);
                    client.sendText(message.from, `Seu Token 🔑 de Acesso é: *${opacao}* `);                      
                }
                else if (message.body == "2") {
                    getOpcao(message.from, message.body);
                    client.sendContactVcard(message.from, "5511972280897@c.us");
                }
                else if (message.body == "3") {
                    getOpcao(message.from, message.body);
                    client.sendText(message.from, `Vamos as compras! 🏃`); 
                    client.sendText(message.from, `Digite o número do produto que você deseja comprar: `); 
                    client.sendImage(message.from, "C:\\Users\\lucas.andrade\\source\\repos\\Testes\\Estudos C#\\ZapFarma\\ZapFarmaImg\\7896181900085.jpg","a","1 - Losartana Potássica");                       
                    client.sendImage(message.from, "C:\\Users\\lucas.andrade\\source\\repos\\Testes\\Estudos C#\\ZapFarma\\ZapFarmaImg\\7896181900122.jpg","a","2 - Atenolol");                       
                    client.sendImage(message.from, "C:\\Users\\lucas.andrade\\source\\repos\\Testes\\Estudos C#\\ZapFarma\\ZapFarmaImg\\7896181900160.jpg","a","3 - Risperidona");                                                               
                }
                else{
                    client.sendText(message.from, `Opção inválida! ⚠`);   
                    client.sendText(message.from, "Escolha umas das opções a baixo para continuar:👇 \n\n 1 - Solicitar Token de Acesso \n 2 - Falar com o Suporte \n 3 - Comprar produtos");   
                }
            }
            else if (stage === 3) {
                getOpcao(message.from, 2);
                client.sendText(message.from, `Comprar realizada com Sucesso! ☑\n\nEnviamos sua noto no Email 📧\n\nObrigado 😀!`); 
            }
            else{
                client.sendText(message.from, "✋");                                  
            }
        }                              

        
    });
}

async function getOpcao(number, opcao){
    const response = await axios.get(`http://localhost:61276/api/request/GetOpcao?number=${number}&opcao=${opcao}`);    
    return response.data;
}

async function getStage(number, nome){
    //const response = await axios.get(`http://localhost:61276/api/request`);
    const response = await axios.get(`http://localhost:61276/api/request/GetStage?number=${number}&nome=${nome}`);    
    return response.data;
}

function updateStage(number) {
    const response = axios.post(`http://localhost:61276/api/request?number=${number}`);   
}