
export class Veiculo{
    id!: number;
    placa!: string;
    modelo!: string;
    ano!: string;
    cor!: string;
    tipoVeiculo!: number;
    capacidadePassageiro!: number;
    capacidadeCarga!: number;
}

export class Carro{
    capacidadePassageiro!: number;
}

export class Caminhao{
    capacidadeCarga!: number;
}