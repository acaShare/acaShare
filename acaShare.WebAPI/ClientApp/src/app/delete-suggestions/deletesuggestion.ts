export interface DeleteSuggestion {
    deleteRequestId: number,
    materialName: string,
    reasonId: number,
    reason: string,
    additionalComment: string,
    deleterName: string,
    requestDate: Date
}