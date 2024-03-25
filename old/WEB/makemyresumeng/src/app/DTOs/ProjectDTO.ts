export class Project {
    name!: string;
    description!: string;
    technologiesUsed!: string[]

    constructor() {
        this.name = "";
        this.description = "";
        this.technologiesUsed = [""]
    }
}