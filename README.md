# Validador de CPF - Azure Functions

Este projeto implementa uma **Azure Function** para validar números de **CPF** enviados via requisições **HTTP POST**.

## 📌 Tecnologias Utilizadas

- **C#** (.NET 8)
- **Azure Functions** (Isolated Worker)
- **Newtonsoft.Json** para manipulação de JSON
- **Postman** para testes (opcional)

## 🚀 Como Executar Localmente

### Pré-requisitos

- **.NET 8 SDK** instalado
- **Azure Functions Core Tools**
- **Conta no Azure** (para deploy)

### Passos

1. **Clone o repositório**:

   ```sh
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio/httpfuncvalidacpf
   ```

2. **Restaure as dependências**:

   ```sh
   dotnet restore
   ```

3. **Inicie a função localmente**:

   ```sh
   func start
   ```

## 📤 Como Fazer o Deploy para o Azure

1. **Login no Azure**:

   ```sh
   az login
   ```

2. **Criação da Function App no Azure** (caso ainda não tenha criado):

   ```sh
   az functionapp create --resource-group SEU_GRUPO --consumption-plan-location eastus --runtime dotnet-isolated --functions-version 4 --name SEU_NOME_FUNCAO --storage-account SUA_STORAGE_ACCOUNT
   ```

3. **Publique a função**:

   ```sh
   func azure functionapp publish SEU_NOME_FUNCAO
   ```

## 📝 Exemplo de Requisição (JSON)

Após o deploy, use a URL gerada pelo Azure para testar sua função.

### **Body (JSON)**:

```json
{
  "cpf": "845.504.230-30"
}
```

### **Resposta Sucesso (200 OK)**:

```json
{
  "message": "CPF válido."
}
```

### **Erros Possíveis**:

- **400 Bad Request**: CPF ausente ou inválido
- **500 Internal Server Error**: Erro inesperado no servidor

## 🔖 Observações

- **Esta função só aceita requisições HTTP POST**.
- **Os CPFs informados podem ser formatados (com pontos e traços) ou não**.
- **Para validar um CPF, a função aplica a lógica de cálculo dos dígitos verificadores**.

---

## **Autor** 👨🏻‍💻
<p>
    <img 
      align=left 
      margin=10 
      width=80 
      src="https://avatars.githubusercontent.com/u/79777133?v=4"
    />
    <p>&nbsp&nbsp&nbspEduardo Ramos<br>
    &nbsp&nbsp&nbsp
    <a 
        href="https://github.com/edugramosf">
        GitHub
    </a>
    &nbsp;|&nbsp;
    <a 
        href="https://www.linkedin.com/in/eduardo-ramos-code/">
        LinkedIn
    </a>
    &nbsp;|&nbsp;
    <a 
        href="https://www.dio.me/users/edugramosf">
        DIO
    </a>
    &nbsp;|&nbsp;</p>
</p>
<br/><br/>
<p>

