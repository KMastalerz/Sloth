export interface SaveBugParam {
    bugID: number;
    clientChanged: boolean;
    newClientID: string | null;
    priorityChanged: boolean;
    newPriorityID: number | null;
    statusChanged: boolean;
    newStatusID: number | null;
    newFunctionalityIDs: number[];
    removedFunctionalityIDs: number[];
    newProductIDs: number[];
    removedProductIDs: number[];
    newHeader: string | null;
    newDescription: string | null;
}