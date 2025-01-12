export interface CreateQuickJobParam {
    type: string,
    header: string,
    description: string,
    priorityID: number,
    products: number[],
    file: File,
    isClient: boolean,
    clientID?: string,
    reportedDate: Date
}