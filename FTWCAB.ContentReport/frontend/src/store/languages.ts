import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import type { RootState } from '@/store/configureStore'
import type { Language } from '@/data/data'

export interface LanguagesState {
    languages: Language[]
    selectedLanguage: Language | null
}

const initialState: LanguagesState = {
    languages: [],
    selectedLanguage: null,
}

const fetchLanguages = createAsyncThunk('contentTypes/fetchLanguages', async () => {
    const response = await fetch('/EPiServer/FTWCAB.ContentReport/ContentUsagesApi/GetSiteLanguages');
    return (await response.json()) as Language[];
});

export const languagesSlice = createSlice({
    name: 'languages',
    initialState,
    reducers: {
        setSelectedLanguage: (state, action) => {
            state.selectedLanguage = action.payload;
        },
    },
    extraReducers: (builder) => builder.addCase(fetchLanguages.fulfilled, (state, action) => {
        state.languages = action.payload;
    }),
})

export { fetchLanguages };
export const { setSelectedLanguage } = languagesSlice.actions;
export const languages = (state: RootState) => state.languages.languages;
export const selectedLanguage = (state: RootState) => state.languages.selectedLanguage;

export default languagesSlice.reducer
