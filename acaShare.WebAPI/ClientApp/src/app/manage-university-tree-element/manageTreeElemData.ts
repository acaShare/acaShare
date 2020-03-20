import { UniversityTreeElement } from "./universityTreeElement";

export class ManageTreeElemData {
    headerActionName: string;
    whatToAddOrEdit: string;
    backActionName: string;
    submitActionName: string;
    model: UniversityTreeElement = new UniversityTreeElement();
}