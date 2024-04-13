import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import type { RootState } from '@/store/configureStore'
import type { ContentType, ContentGroupType } from '@/data/data'

export interface ContentTypesState {
    types: ContentGroupType[]
    selectedType: ContentType | null
}

const initialState: ContentTypesState = {
    types: [],
    selectedType: null,
}

const fetchContentTypeGroups = createAsyncThunk('contentTypes/fetchContentTypeGroups', async () => {
    const response = await fetch('/EPiServer/FTWCAB.ContentReport/ContentUsagesApi/GetContentTypes');
    return (await response.json()) as ContentGroupType[];
});

export const contentTypesSlice = createSlice({
    name: 'contentTypes',
    initialState,
    reducers: {
        setSelectedType: (state, action) => {
            state.selectedType = action.payload;
        },
    },
    extraReducers: (builder) => builder.addCase(fetchContentTypeGroups.fulfilled, (state, action) => {
        state.types = action.payload;
        // state.selectedType = {
        //     "id": 19,
        //     "name": "StandardPage",
        //     "fullName": "[Default] Standard Page"
        // };
    }),
})

export { fetchContentTypeGroups };
export const { setSelectedType } = contentTypesSlice.actions;
export const types = (state: RootState) => state.contentTypes.types
export const selectedType = (state: RootState) => state.contentTypes.selectedType

export default contentTypesSlice.reducer
