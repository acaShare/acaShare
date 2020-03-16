export interface DeleteSuggestionApprovalDecision {
    material: MaterialViewModel,
    deleteSuggestion: DeleteRequestViewModel
}

export interface MaterialViewModel {
    materialId: number,
    creatorUsername: string,
    name: string,
    description: string,
    uploadDate: Date,
    files: FileViewModel[]
}

export class FileViewModel {
    constructor(
        public fileId: number,
        public fileName: string,
        public relativePath: string,
        public contentType: string,
    ) {}
    
    isImage: boolean = this.contentType.startsWith("image");
}

export interface DeleteRequestViewModel {
    deleteRequestId: number,
    reasonId: number,
    reason: string,
    additionalComment: string,
    deleterName: string
}