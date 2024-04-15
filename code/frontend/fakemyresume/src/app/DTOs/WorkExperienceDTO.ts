import { Project } from "./ProjectDTO";


export class WorkExperience {
    dateBegin?: Date;
    dateEnd?: Date;
    companyName?: string;
    projectName?: string;
    role?: string;
    description?: string;
    projects?: Project[];
}