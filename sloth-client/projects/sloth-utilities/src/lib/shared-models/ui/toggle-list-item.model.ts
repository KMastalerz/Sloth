export interface ToggleListItem {
    value: string;
    label?: string;
    class?: 'lowest' | 'low' | 'medium' | 'high' | 'critical';
}