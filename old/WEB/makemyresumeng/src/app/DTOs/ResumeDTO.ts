import { Education } from "./EducationDTO";
import { WorkExperience } from "./WorkExperienceDTO";

export class ResumeDTO {
    accountId!: string;
    fullname!: string;
    currentRole!: string;
    email!: string;
    description!: string;
    technologies!: string[];
    methodologies!: string[];
    certifications!: string[];
    workExperience!: WorkExperience[];
    education!: Education[];

    constructor() {
        this.fullname = "";
        this.currentRole = "";
        this.email = "";
        this.description = "";
        this.technologies = [];
        this.methodologies = [""];
        this.certifications = [""];
        this.workExperience = [];
        this.education = [];
    }
}






