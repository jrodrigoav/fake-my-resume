import { Education } from "./EducationDTO";
import { WorkExperience } from "./WorkExperienceDTO";

export class ResumeDTO {
    id?: number;
    accountId?: string;
    fullName!: string;
    currentRole!: string;
    email!: string;
    description!: string;
    certifications!: string[];
    workExperience!: WorkExperience[];
    education!: Education[];

    constructor() {
        this.fullName = "";
        this.currentRole = "";
        this.email = "";
        this.description = "";
        this.certifications = [];
        this.workExperience = [];
        this.education = [];
    }
}






