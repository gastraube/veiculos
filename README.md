# Cadastro de Veículos
Aplicação para inclusão, alteração, edição e exclusão de veículos

## Como rodar

Tenha o **Docker** instalado na sua máquina.

Para rodar o **Backend** e o **Banco de Dados**:

Na raiz do projeto, rodar:

```bash
docker compose --project-name veiculos up -d
```

Para rodar o **Frontend**:

Na pasta **Veiculos/Front** Executar
```bash
docker build . -t veiculos-frontend
```

Em seguida
```bash
docker run -p 4200:4200 --name veiculos-frontend veiculos-frontend
```

Agora é só acessar o endereço http://localhost:4200 pelo browser.

## Arquitetura: 
![Solution Architecture](https://raw.githubusercontent.com/gastraube/veiculos/refs/heads/main/veiculos-arquitetura.jpg)
