import './Header.css';
import { useEffect } from 'react';
import Select from 'react-select'
import { ContentType, Language } from '@/data/data';
import { useAppSelector, useAppDispatch } from '@/hooks'
import { fetchContentTypeGroups, setSelectedType } from '@/store/contentTypes'
import { fetchLanguages, setSelectedLanguage } from '@/store/languages'

const Header = () => {
  const dispatch = useAppDispatch();
  const contentTypeGroups = useAppSelector(state => state.contentTypes).types;
  const { languages, selectedLanguage } = useAppSelector(state => state.languages);

  useEffect(() => {
    dispatch(fetchContentTypeGroups());
    dispatch(fetchLanguages());
  }, [dispatch]);

  return (
    <div className="epi-main-header Header">
      <header className="axiom-typography--header1">
        Content Usages
      </header>
      <div style={{ width: '300px' }}>
        <Select
          className="Header__language-select"
          isClearable
          placeholder="Select language type"
          getOptionLabel={(option: Language) => option.name}
          getOptionValue={(option: Language) => option.id}
          options={languages}
          value={selectedLanguage}
          onChange={(l) => dispatch(setSelectedLanguage(l))}
        />
        <Select
          isClearable
          placeholder="Select content type"
          getOptionLabel={(option: ContentType) => option.name}
          getOptionValue={(option: ContentType) => option.id.toString()}
          options={contentTypeGroups}
          onChange={(o) => dispatch(setSelectedType(o))}
        />
      </div>
    </div>
  );
}

export default Header;
