export interface CreateJobParam {
    type: string,
    header: string,
    description: string,
    priorityID: number,
    products: number[],
    files: FileList,
    clientID?: string,
    reportedDate: Date
}