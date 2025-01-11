export interface CreateQuickJobParam {
    type: string,
    header: string,
    description: string,
    priority: string,
    products: number[],
    file: File,
    isClient: boolean,
    clientID?: string
}