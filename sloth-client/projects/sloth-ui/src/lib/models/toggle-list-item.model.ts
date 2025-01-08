export interface ToggleListItem {
    value: string;
    display?: string;
    class?: 'lowest' | 'low' | 'medium' | 'high' | 'critical';
}