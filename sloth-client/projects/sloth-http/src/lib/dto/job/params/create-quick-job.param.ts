export interface CreateQuickJobParam {
    type: string,
    header: string,
    description: string,
    priorityID: number,
    products: number[],
    files: FileList,
    clientID?: string,
    reportedDate: Date
}