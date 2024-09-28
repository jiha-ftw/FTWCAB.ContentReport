import { configureStore } from '@reduxjs/toolkit'
import contentTypes from './contentTypes'
import contentInstances from './contentInstance';
import contentUsage from './contentUsage';
import languages from './languages';

const store = configureStore({
  reducer: {
    contentTypes,
    contentInstances,
    contentUsage,
    languages,
  }
})

export default store;

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
